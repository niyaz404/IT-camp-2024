/**
 * Генератор случайного цвета
 * @returns случайный цвет
 */
export const getRandomColor = () => {
  return `rgb(${getRandomNum()},${getRandomNum()},${getRandomNum()})`;
};

/**
 * Генерирует случайно число в диапозоне от 0 до 255
 * @returns случайно число в диапозоне от 0 до 255
 */
const getRandomNum = () => {
  const min = 0;
  const max = 255;
  return Math.floor(Math.random() * (max - min + 1)) + min;
};
