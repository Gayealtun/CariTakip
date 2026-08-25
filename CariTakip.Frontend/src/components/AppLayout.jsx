import {
  NavLink,
  Outlet,
  useNavigate,
} from "react-router-dom";

import { logout } from "../utils/auth.js";

function AppLayout() {
  const navigate = useNavigate();

  function handleLogout() {
    logout();
    navigate("/", { replace: true });
  }

  return (
    <div className="app-layout">
      <aside className="sidebar">
        <div className="sidebar-brand">
          <div className="sidebar-logo">CT</div>

          <div>
            <strong>Cari Takip</strong>
            <span>Yönetim Sistemi</span>
          </div>
        </div>

        <nav className="sidebar-nav">
  <NavLink
    to="/profile"
    className={({ isActive }) =>
      isActive
        ? "sidebar-link active"
        : "sidebar-link"
    }
  >
    Profilim
  </NavLink>

  <NavLink
    to="/cariler"
    className={({ isActive }) =>
      isActive
        ? "sidebar-link active"
        : "sidebar-link"
    }
  >
    Cari Listesi
  </NavLink>

  <div className="sidebar-info">
    Cari hareketlerine, cari listesindeki
    “Hareketler” butonundan ulaşabilirsiniz.
  </div>
</nav>

<button
  className="sidebar-logout"
  type="button"
  onClick={handleLogout}
>
  Çıkış Yap
</button>

      </aside>

      <main className="app-content">
        <Outlet />
      </main>
    </div>
  );
}

export default AppLayout;