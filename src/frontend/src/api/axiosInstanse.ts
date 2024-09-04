import axios from "axios";

const baseURL = "http://localhost:3000/";

export const axiosInstance = axios.create({
  baseURL,
  responseType: "json",
});

// interceptor для добавление токена в хедер запросов
axiosInstance.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.authorization = "Bearer " + token;
  }
  return config;
});

// interceptor для перехвата ошибок авторизации
axios.interceptors.response.use(
  (response) => response,
  (error) => {
    const originalRequest = error.config;
    if (error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      // Проверка токена
      const token = localStorage.getItem("token");
      if (token) {
        localStorage.removeItem("token");

        // const decodedToken = jwtDecode(token);
        // const isExpired = decodedToken.exp * 1000 < Date.now();
        // if (isExpired) {
        //   localStorage.removeItem('token');
        //   // Перенаправить пользователя на страницу входа
        //   navigate('/login');
        //   return Promise.reject(error);
        // }
      }

      // Попытка обновления токена (если требуется)
      // return axios
      //   .post('/api/refresh-token')
      //   .then(response => {
      //     const newToken = response.data.token;
      //     localStorage.setItem('token', newToken);
      //     axios.defaults.headers.common['Authorization'] = `Bearer ${newToken}`;
      //     return axios(originalRequest);
      //   })
      //   .catch(err => {
      //     localStorage.removeItem('token');
      //     navigate('/login');
      //     return Promise.reject(error);
      //   });
    }
    return Promise.reject(error);
  }
);
