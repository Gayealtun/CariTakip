export function getToken() {
  return localStorage.getItem("token");
}

export function logout() {
  localStorage.removeItem("token");
}

export function isTokenValid() {
  const token = getToken();

  if (!token) {
    return false;
  }

  try {
    const payloadPart = token.split(".")[1];

    if (!payloadPart) {
      return false;
    }

    const normalizedPayload = payloadPart
      .replace(/-/g, "+")
      .replace(/_/g, "/");

    const payload = JSON.parse(atob(normalizedPayload));

    if (!payload.exp) {
      return false;
    }

    const currentTime = Math.floor(Date.now() / 1000);

    return payload.exp > currentTime;
  } catch {
    return false;
  }
}