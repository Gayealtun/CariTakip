import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import {
  getHareketlerByCariId,
  createCariHareket,
  deleteCariHareket,
  updateCariHareket,
  getBakiyeByCariId,
} from "../services/cariHareketService.js";

function CariHareketPage() {
  const { cariId } = useParams();
  const navigate = useNavigate();

  const [hareketler, setHareketler] = useState([]);
  const [error, setError] = useState("");

  const [tip, setTip] = useState(1);
  const [kaynak, setKaynak] = useState(5);
  const [tutar, setTutar] = useState("");
  const [aciklama, setAciklama] = useState("");

  const [editingId, setEditingId] = useState(null);
  const [bakiye, setBakiye] = useState(0);

  async function loadPageData() {
    try {
      const hareketData =
        await getHareketlerByCariId(cariId);

      const bakiyeData =
        await getBakiyeByCariId(cariId);

      setHareketler(hareketData);
      setBakiye(bakiyeData.bakiye);
      setError("");
    } catch (error) {
      setError(error.message);
    }
  }

  useEffect(() => {
    loadPageData();
  }, [cariId]);

  function handleEditHareket(hareket) {
    setEditingId(hareket.id);
    setTip(hareket.tip);
    setKaynak(hareket.kaynak);
    setTutar(hareket.tutar);
    setAciklama(hareket.aciklama ?? "");
  }

  async function handleCreateHareket(event) {
    event.preventDefault();

    const hareket = {
      cariId: Number(cariId),
      tarih: new Date().toISOString(),
      tip: Number(tip),
      kaynak: Number(kaynak),
      tutar: Number(tutar),
      aciklama,
      kaynakId: null,
    };

    try {
      if (editingId === null) {
        await createCariHareket(hareket);
      } else {
        await updateCariHareket(editingId, hareket);
        setEditingId(null);
      }

      setTip(1);
      setKaynak(5);
      setTutar("");
      setAciklama("");
      setError("");

      await loadPageData();
    } catch (error) {
      setError(error.message);
    }
  }

  async function handleDeleteHareket(id) {
    const onay = window.confirm(
      "Bu hareketi silmek istediğinize emin misiniz?"
    );

    if (!onay) {
      return;
    }

    try {
      await deleteCariHareket(id);
      await loadPageData();
    } catch (error) {
      setError(error.message);
    }
  }

  return (
    <div className="page">
      <div className="card movement-card">
        <div className="page-header">
          <h1>Cari Hareketleri</h1>
        </div>

        <h2
          className={
            bakiye > 0
              ? "balance debt"
              : bakiye < 0
              ? "balance credit"
              : "balance zero"
          }
        >
          Bakiye: {Math.abs(bakiye)} TL{" "}
          {bakiye > 0
            ? "Borçlu"
            : bakiye < 0
            ? "Alacaklı"
            : "Bakiye Kapalı"}
        </h2>

        <form
          className="movement-form"
          onSubmit={handleCreateHareket}
        >
          <h2>
            {editingId === null
              ? "Yeni Hareket Ekle"
              : "Hareket Güncelle"}
          </h2>

          <div className="movement-field">
            <label htmlFor="tip">Hareket Türü</label>

            <select
              id="tip"
              value={tip}
              onChange={(event) =>
                setTip(event.target.value)
              }
            >
              <option value={1}>Borç</option>
              <option value={2}>Alacak</option>
            </select>
          </div>

          <div className="movement-field">
            <label htmlFor="kaynak">Kaynak Türü</label>

            <select
              id="kaynak"
              value={kaynak}
              onChange={(event) =>
                setKaynak(event.target.value)
              }
            >
              <option value={1}>Satış Faturası</option>
              <option value={2}>Alış Faturası</option>
              <option value={3}>Tahsilat</option>
              <option value={4}>Ödeme</option>
              <option value={5}>Manuel İşlem</option>
            </select>
          </div>

          <div className="movement-field">
            <label htmlFor="tutar">Tutar</label>

            <input
              id="tutar"
              type="number"
              placeholder="Tutar giriniz"
              value={tutar}
              onChange={(event) =>
                setTutar(event.target.value)
              }
              required
            />
          </div>

          <div className="movement-field movement-description">
            <label htmlFor="aciklama">Açıklama</label>

            <input
              id="aciklama"
              type="text"
              placeholder="Açıklama giriniz"
              value={aciklama}
              onChange={(event) =>
                setAciklama(event.target.value)
              }
            />
          </div>

          <button
            className="movement-submit"
            type="submit"
          >
            {editingId === null
              ? "Hareket Ekle"
              : "Güncellemeyi Kaydet"}
          </button>
        </form>

        {error && <p className="login-error">{error}</p>}

        {hareketler.length === 0 ? (
          <p>Bu cariye ait hareket bulunamadı.</p>
        ) : (
          <table className="data-table">
            <thead>
              <tr>
                <th>Id</th>
                <th>Tarih</th>
                <th>Tip</th>
                <th>Kaynak</th>
                <th>Açıklama</th>
                <th>Tutar</th>
                <th>İşlemler</th>
              </tr>
            </thead>

            <tbody>
              {hareketler.map((hareket) => (
                <tr key={hareket.id}>
                  <td>{hareket.id}</td>

                  <td>
                    {new Date(
                      hareket.tarih
                    ).toLocaleString("tr-TR")}
                  </td>

                  <td>
                    {hareket.tip === 1
                      ? "Borç"
                      : "Alacak"}
                  </td>

                  <td>
                    {hareket.kaynak === 1 &&
                      "Satış Faturası"}
                    {hareket.kaynak === 2 &&
                      "Alış Faturası"}
                    {hareket.kaynak === 3 &&
                      "Tahsilat"}
                    {hareket.kaynak === 4 &&
                      "Ödeme"}
                    {hareket.kaynak === 5 &&
                      "Manuel İşlem"}
                  </td>

                  <td>{hareket.aciklama}</td>
                  <td>{hareket.tutar} TL</td>

                  <td>
                    <button
                      className="edit-button"
                      type="button"
                      onClick={() =>
                        handleEditHareket(hareket)
                      }
                    >
                      Düzenle
                    </button>

                    <button
                      className="delete-button"
                      type="button"
                      onClick={() =>
                        handleDeleteHareket(
                          hareket.id
                        )
                      }
                    >
                      Sil
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}

        <button
          className="back-button"
          type="button"
          onClick={() => navigate("/cariler")}
        >
          Cari Listesine Dön
        </button>
      </div>
    </div>
  );
}

export default CariHareketPage;