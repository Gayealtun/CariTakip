const API_URL = "http://localhost:5230/api/Users";

export async function login(userName, password) {
  const response = await fetch(`${API_URL}/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      userName,
      password,
    }),
  });

  if (!response.ok) {
    throw new Error("Kullanıcı adı veya şifre hatalı.");
  }

  return await response.json();
}
export async function registerUser(user) {
  const response = await fetch(
    "http://localhost:5230/api/Users",
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(user),
    }
  );

  if (!response.ok) {
  const errorText = await response.text();
  console.log("Backend hatası:", errorText);
  throw new Error(errorText || "Kullanıcı oluşturulamadı.");
}

  return await response.json();
}
export async function getProfile() {
  const token = localStorage.getItem("token");

  const response = await fetch(
    "http://localhost:5230/api/Users/me",
    {
      method :"GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }
  );

  if (!response.ok) {
    const errorText = await response.text();
    console.log(
      "Profile GET hatası",
      response.status,
      errorText
    );

    throw new Error(
      "Profil alınamadı. Hata kodu: ${response.status}`"
    );
  }

  return await response.json();
}

export async function updateProfile(profile) {
  const token = localStorage.getItem("token");

  const response = await fetch(
    "http://localhost:5230/api/Users/me",
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(profile),
    }
  );

  if (!response.ok) {
    throw new Error("Profil güncellenemedi.");
  }

  return await response.json();
}