import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { registerUser } from "../services/authServices.js";

function RegisterPage() {
  const navigate = useNavigate();

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [gender, setGender] = useState("");
  const [birthDate, setBirthDate] = useState("");
  const [nationalId, setNationalId] = useState("");
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  async function handleRegister(event) {
    event.preventDefault();

    if (nationalId.length !== 11) {
  setError("TC Kimlik No 11 haneli olmalıdır.");
  return;
}

    try {
      await registerUser({
        firstName,
        lastName,
        gender,
        birthDate,
        nationalId,
        userName,
        password,
      });

      alert("Kullanıcı oluşturuldu.");
      navigate("/");
    } catch (error) {
      setError(error.message);
    }
  }

  return (
    <div className="page">
      <div className="card register card">
        <h1>Kullanıcı Kaydı</h1>

        <form className="form-grid register-form" onSubmit={handleRegister}>
          <input
            type="text"
            placeholder="Ad"
            value={firstName}
            onChange={(event) => setFirstName(event.target.value)}
            required
          />

          <input
            type="text"
            placeholder="Soyad"
            value={lastName}
            onChange={(event) => setLastName(event.target.value)}
            required
          />

        <select
  value={gender}
  onChange={(event) => setGender(event.target.value)}
  required
>
  <option value="">Cinsiyet seçiniz</option>
  <option value="Kadın">Kadın</option>
  <option value="Erkek">Erkek</option>
</select>
          <select
            type="date"
            value={birthDate}
            onChange={(event) => setBirthDate(event.target.value)}
            required
          > 
          <option value="">Cinsiyet seçiniz</option>
          <option value="Kadın">Kadın</option>
          <option value="Erkek">Erkek</option>
          </select>

          <input
  type="text"
  placeholder="TC Kimlik No"
  value={nationalId}
  maxLength={11}
  onChange={(event) => {
    const value = event.target.value.replace(/\D/g, "");
    setNationalId(value.slice(0, 11));
  }}
  required
/>

          <input
            type="text"
            placeholder="Kullanıcı adı"
            value={userName}
            onChange={(event) => setUserName(event.target.value)}
            required
          />

          <input
            type="password"
            placeholder="Şifre"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
            required
          />

          <button type="submit">Kayıt Ol</button>
        </form>

        {error && <p>{error}</p>}

        <button
          type="button"
          onClick={() => navigate("/")}
        >
          Giriş Sayfasına Dön
        </button>
      </div>
    </div>
  );
}

export default RegisterPage;