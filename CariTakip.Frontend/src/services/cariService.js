const API_URL = "http://localhost:5230/api/Cari";

export async function getCariler() {
  const token = localStorage.getItem("token");

  const response = await fetch(API_URL, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Cariler alınamadı.");
  }

  return await response.json();
}

export async function createCari(cari) {
  const token = localStorage.getItem("token");

  const response = await fetch(API_URL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(cari),
  });

  if (!response.ok) {
  const errorText = await response.text();

  console.log(
    "Cari oluşturma hatası:",
    response.status,
    errorText
  );

  throw new Error(
    errorText || `Cari oluşturulamadı. (${response.status})`
  );
}

  return await response.json();
}

export async function updateCari(id, cari) {
  const token = localStorage.getItem("token");

  const response = await fetch(`${API_URL}/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(cari),
  });

  if (!response.ok) {
    throw new Error("Cari güncellenemedi.");
  }
}

export async function deleteCari(id) {
  const token = localStorage.getItem("token");

  const response = await fetch(`${API_URL}/${id}`, {
    method: "DELETE",
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Cari silinemedi.");
  }
}
export async function getCariById(id) {
  const token = localStorage.getItem("token");

  const response = await fetch(
    `http://localhost:5230/api/Cari/${id}`,
    {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }
  );

  if (!response.ok) {
    throw new Error("Cari bilgisi alınamadı.");
  }

  return await response.json();
}