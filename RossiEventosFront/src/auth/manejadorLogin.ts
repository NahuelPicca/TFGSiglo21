export const isAuthenticated = () => {
    const user = localStorage.getItem('user');
    return !!user; // Verifica si hay un usuario almacenado en localStorage
  };
  
  export const login = (username: string, password: string) => {
    // Lógica de autenticación (puede ser una llamada a un API)
    // Simulación básica para demostración
    if (username === 'usuario' && password === 'contraseña') {
      localStorage.setItem('user', username);
      return true;
    } else {
      return false;
    }
  };
  
  export const logout = () => {
    localStorage.removeItem('user');
  };