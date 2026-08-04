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
  const [kredilimiti, setKredilimiti] = useState("");
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
    setKredilimiti(cari.kredilimiti);
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
      aktifMi: true,
      krediLimiti: Number(kredilimiti),
    };

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
      setKredilimiti("");
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
          placeholder="Vergi No / TC"
          value={vergiNoTC}
          onChange={(event) => setVergiNoTC(event.target.value)}
          required
        />

        <input
          type="text"
          placeholder="Telefon"
          value={telefon}
          onChange={(event) => setTelefon(event.target.value)}
        />

        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(event) => setEmail(event.target.value)}
        />

        <input
          type="number"
          placeholder="Kredi Limiti"
          value={kredilimiti}
          onChange={(event) =>
            setKredilimiti(event.target.value)
          }
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
              <th>Id</th>
              <th>Ünvan</th>
              <th>Telefon</th>
              <th>Email</th>
              <th>Kredi Limiti</th>
              <th>İşlemler</th>
            </tr>
          </thead>

          <tbody>
            {cariler.map((cari) => (
              <tr key={cari.id}>
                <td>{cari.id}</td>
                <td>{cari.unvan}</td>
                <td>{cari.telefon}</td>
                <td>{cari.email}</td>
                <td>{cari.kredilimiti}</td>

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