import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../services/authServices.js";

function LoginPage() {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const navigate = useNavigate();

  async function handleSubmit(event) {
    event.preventDefault();
    setError("");

    try {
      const data = await login(userName, password);

      localStorage.setItem("token", data.token);

      navigate("/cariler");
    } catch (error) {
      setError(error.message);
    }
  }

  return (
    <div className="login-page">
      <div className="login-card">
        <div className="login-logo">CT</div>

        <h1>Cari Takip Sistemi</h1>

        <p className="login-subtitle">
          Cari ve hareket yönetimi uygulaması
        </p>

        <form className="login-form" onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="userName">Kullanıcı adı</label>

            <input
              id="userName"
              type="text"
              value={userName}
              onChange={(event) => setUserName(event.target.value)}
              placeholder="Kullanıcı adınızı giriniz"
              required
            />
          </div>

          <div className="form-group">
            <label htmlFor="password">Şifre</label>

            <input
              id="password"
              type="password"
              value={password}
              onChange={(event) => setPassword(event.target.value)}
              placeholder="Şifrenizi giriniz"
              required
            />
          </div>

          {error && (
            <p className="login-error">
              {error}
            </p>
          )}

          <button
            className="login-button"
            type="submit"
          >
            Giriş Yap
          </button>

          <button
            className="register-button"
            type="button"
            onClick={() => navigate("/register")}
          >
            Yeni Kullanıcı Oluştur
          </button>
        </form>

        <p className="login-footer">
          Cari Takip Staj Projesi · 2026
        </p>
      </div>
    </div>
  );
}

export default LoginPage;