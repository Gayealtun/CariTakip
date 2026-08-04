const API_URL = "http://localhost:5230/api/CariHareket";

export async function getHareketlerByCariId(cariId) {
  const token = localStorage.getItem("token");

  const response = await fetch(
    `${API_URL}/cari/${cariId}`,
    {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }
  );

  if (!response.ok) {
    throw new Error("Cari hareketleri alınamadı.");
  }

  return await response.json();
}

export async function createCariHareket(hareket) {
  const token = localStorage.getItem("token");

  const response = await fetch(API_URL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(hareket),
  });

  if (!response.ok) {
    throw new Error("Cari hareketi oluşturulamadı.");
  }

  return await response.json();
}

export async function deleteCariHareket(id) {
  const token = localStorage.getItem("token");

  const response = await fetch(`${API_URL}/${id}`, {
    method: "DELETE",
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Cari hareketi silinemedi.");
  }
}

export async function updateCariHareket(id, hareket) {
  const token = localStorage.getItem("token");

  const response = await fetch(`${API_URL}/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(hareket),
  });

  if (!response.ok) {
    throw new Error("Cari hareketi güncellenemedi.");
  }
}

export async function getBakiyeByCariId(cariId) {
  const token = localStorage.getItem("token");

  const response = await fetch(
    `${API_URL}/cari/${cariId}/bakiye`,
    {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }
  );

  if (!response.ok) {
    throw new Error("Bakiye alınamadı.");
  }

  return await response.json();
}