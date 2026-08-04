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
    throw new Error("Kullanıcı oluşturulamadı.");
  }

  return await response.json();
}