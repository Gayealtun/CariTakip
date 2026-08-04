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
    throw new Error("Cari oluşturulamadı.");
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