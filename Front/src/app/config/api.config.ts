// Конфигурация API
// В Docker используем относительный путь (nginx проксирует), в локальной разработке - localhost
const isDocker = window.location.hostname !== 'localhost' && window.location.hostname !== '127.0.0.1';
const apiBaseUrl = isDocker 
  ? '/api'  // Для Docker - nginx проксирует запросы
  : 'http://localhost:5066/api';  // Для локальной разработки

export const API_CONFIG = {
  baseUrl: apiBaseUrl
};
