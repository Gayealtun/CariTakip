import { useEffect, useState } from "react";
import {
  updateCari,
  createCari,
  getCariler,
  deleteCari,
} from "../services/cariService.js";
import { useNavigate } from "react-router-dom";

function CariPage() {
  const [cariler, setCariler] = useState([]);
  const [error, setError] = useState("");

  const [unvan, setUnvan] = useState("");
  const [vergiNoTC, setVergiNoTC] = useState("");
  const [telefon, setTelefon] = useState("");
  const [email, setEmail] = useState("");
  const [krediLimiti, setKrediLimiti] = useState("");
  const navigate = useNavigate();



  const [editingId, setEditingId] = useState(null);

  async function loadCariler() {
    try {
      const data = await getCariler();

      setCariler(data);
      setError("");
    } catch (error) {
      setError(error.message);
    }
  }

  useEffect(() => {
    loadCariler();
  }, []);

  function handleEdit(cari) {
    setEditingId(cari.id);

    setUnvan(cari.unvan);
    setVergiNoTC(cari.vergiNoTC);
    setTelefon(cari.telefon);
    setEmail(cari.email);
    setKrediLimiti(cari.krediLimiti);
  }

  async function handleDelete(id) {
  const onay = window.confirm(
    "Bu cariyi silmek istediğinize emin misiniz?"
  );

  if (!onay) {
    return;
  }

  try {
    await deleteCari(id);
    await loadCariler();
  } catch (error) {
    setError(error.message);
  }
}

  async function handleCreateCari(event) {
    event.preventDefault();

    const cariData = {
      unvan,
      vergiNoTC,
      adres: "",
      telefon,
      email,
      tip: 0,
      iban: "",
      krediLimiti: Number(krediLimiti),
    };
    console.log("Gönderilen cari:", cariData);

    try {
      if (editingId === null) {
        await createCari(cariData);
      } else {
        await updateCari(editingId, cariData);
        setEditingId(null);
      }

      setUnvan("");
      setVergiNoTC("");
      setTelefon("");
      setEmail("");
      setKrediLimiti("");
      setError("");

      await loadCariler();
    } catch (error) {
      setError(error.message);
    }
  }

  return (
    <div className="page">
    <div className="card cari-card">
      <h1>Cari Listesi</h1>

      <div className="page-header">
  

  
  
</div>

      <form className="form-grid cari-form" onSubmit={handleCreateCari}>
        <h2>
          {editingId === null
            ? "Yeni Cari Ekle"
            : "Cari Güncelle"}
        </h2>

        <input
          type="text"
          placeholder="Ünvan"
          value={unvan}
          onChange={(event) => setUnvan(event.target.value)}
          required
        />

        <input
  type="text"
  placeholder="Vergi No"
  value={vergiNoTC}
  maxLength={10}
  onChange={(event) => {
    const value = event.target.value.replace(/\D/g, "");
    setVergiNoTC(value.slice(0, 10));
  }}
/>

        <input
  type="tel"
  placeholder="5** *** ** **"
  value={telefon}
  onChange={(event) => {
    let value = event.target.value.replace(/\D/g, "");

    value = value.slice(0, 10);

    if (value.length > 6) {
      value =
        value.slice(0, 3) +
        " " +
        value.slice(3, 6) +
        " " +
        value.slice(6, 8) +
        " " +
        value.slice(8, 10);
    } else if (value.length > 3) {
      value =
        value.slice(0, 3) +
        " " +
        value.slice(3);
    }

    setTelefon(value);
  }}
  required
/>
       <input
  type="email"
  placeholder="ornek@mail.com"
  value={email}
  onChange={(event) => setEmail(event.target.value)}
  required
/>

        <input
  type="number"
  min="0"
  placeholder="Kredi Limiti"
  value={krediLimiti}
  onChange={(event) => {
    const value = event.target.value;

    if (value === "" || Number(value) >= 0) {
      setKrediLimiti(value);
    }
  }}
/>

        <button type="submit">
          {editingId === null
            ? "Cari Ekle"
            : "Güncellemeyi Kaydet"}
        </button>
      </form>

      {error && <p>{error}</p>}

      {cariler.length === 0 ? (
        <p>Kayıtlı cari bulunamadı.</p>
      ) : (
        <table className="data-table">
          <thead>
            <tr>
              <th>Sıra</th>
              <th>Ünvan</th>
              <th>Telefon</th>
              <th>Email</th>
              <th>Vergi No</th>
              <th>Kredi Limiti</th>
              <th>İşlemler</th>

            </tr>
          </thead>

          <tbody>
            {cariler.map((cari, index) => (
              <tr key={cari.id}>
                <td>{index +1 }</td>
                <td>{cari.unvan}</td>
                <td>{cari.telefon}</td>
                <td>{cari.email}</td>
                <td>{cari.vergiNoTC}</td>
                <td>{cari.krediLimiti} TL</td>

                <td>
                  <button
                    className="edit-button"
                    type="button"
                    onClick={() => handleEdit(cari)}
                  >
                    Düzenle
                  </button>
                  <button
                    className="movement-button"
                    type="button"
                    onClick={() => navigate(`/cariler/${cari.id}/hareketler`) }
                  >
                Hareketler
                 </button>
                  <button
                    className="delete-button"
                    type="button"
                 onClick={() => handleDelete(cari.id)}
                  >
                    Sil
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
     </div>
  </div>
);
}

export default CariPage;