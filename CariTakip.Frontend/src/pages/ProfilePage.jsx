import { useEffect, useState } from "react";
import { getProfile, updateProfile } from "../services/authServices.js";
import { useNavigate } from "react-router-dom";

function ProfilePage() {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [gender, setGender] = useState("");
  const [birthDate, setBirthDate] = useState("");
  const [nationalId, setNationalId] = useState("");
  const [userName, setUserName] = useState("");
  const navigate = useNavigate();

  const [message, setMessage] = useState("");
  const [error, setError] = useState("");

  useEffect(() => {
    async function loadProfile() {
      try {
        const data = await getProfile();

        setFirstName(data.firstName ?? "");
        setLastName(data.lastName ?? "");
        setGender(data.gender ?? "");
        setNationalId(data.nationalId ?? "");
        setUserName(data.userName ?? "");

        if (data.birthDate) {
          setBirthDate(data.birthDate.substring(0, 10));
        }

        setError("");
      } catch (error) {
        setError(error.message);
      }
    }

    loadProfile();
  }, []);

  async function handleSubmit(event) {
    event.preventDefault();
    
    if (nationalId.length !== 11) {
  setError("TC Kimlik No 11 haneli olmalıdır.");
  return;
}

    const profile = {
      firstName,
      lastName,
      gender,
      birthDate,
      nationalId,
      userName,
    };

    try {
      const updatedUser = await updateProfile(profile);

      setFirstName(updatedUser.firstName ?? "");
      setLastName(updatedUser.lastName ?? "");
      setGender(updatedUser.gender ?? "");
      setNationalId(updatedUser.nationalId ?? "");
      setUserName(updatedUser.userName ?? "");

      if (updatedUser.birthDate) {
        setBirthDate(updatedUser.birthDate.substring(0, 10));
      }

      setMessage("Profil başarıyla güncellendi.");
      setError("");
    } catch (error) {
      setMessage("");
      setError(error.message);
    }
  }

  return (
    <div className="page">
      <div className="card profile-card">
        <div className="page-header">
          <h1>Profilim</h1>
        </div>

        <form className="profile-form" onSubmit={handleSubmit}>
          <div className="profile-field">
            <label htmlFor="firstName">Ad</label>

            <input
              id="firstName"
              type="text"
              value={firstName}
              onChange={(event) =>
                setFirstName(event.target.value)
              }
              required
            />
          </div>

          <div className="profile-field">
            <label htmlFor="lastName">Soyad</label>

            <input
              id="lastName"
              type="text"
              value={lastName}
              onChange={(event) =>
                setLastName(event.target.value)
              }
              required
            />
          </div>

          <div className="profile-field">
            <label htmlFor="gender">Cinsiyet</label>

            <select
              id="gender"
              value={gender}
              onChange={(event) =>
                setGender(event.target.value)
              }
              required
            >
              <option value="">Seçiniz</option>
              <option value="Kadın">Kadın</option>
              <option value="Erkek">Erkek</option>
            </select>
          </div>

          <div className="profile-field">
            <label htmlFor="birthDate">Doğum Tarihi</label>

            <input
              id="birthDate"
              type="date"
              value={birthDate}
              onChange={(event) =>
                setBirthDate(event.target.value)
              }
              required
            />
          </div>

          <div className="profile-field">
            <label htmlFor="nationalId">TC Kimlik No</label>

            <input
  id="nationalId"
  type="text"
  value={nationalId}
  maxLength={11}
  onChange={(event) => {
    const value = event.target.value.replace(/\D/g, "");
    setNationalId(value.slice(0, 11));
  }}
  required
/>
          </div>

          <div className="profile-field">
            <label htmlFor="userName">Kullanıcı Adı</label>

            <input
              id="userName"
              type="text"
              value={userName}
              onChange={(event) =>
                setUserName(event.target.value)
              }
              required
            />
          </div>

          {error && (
            <p className="profile-error">{error}</p>
          )}

          {message && (
            <p className="profile-success">{message}</p>
          )}

          <button
            className="profile-save-button"
            type="submit"
          >
            Değişiklikleri Kaydet
          </button>

        </form>
        <button
  type="button"
  className="back-button"
  onClick={() => navigate(-1)}
>
  Geri Dön
</button>
      </div>
    </div>
  );
}

export default ProfilePage;