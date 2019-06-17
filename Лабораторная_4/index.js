/**
  * Функция для кодирования с помощью перестановочного шифра
  * @param str - строка открытого текста
  * @param key - ключ (количество строк матрицы)
  * @param decode - default : true - кодирование или декодирование
*/
function reshuffle(str, key, decode = true) {
    const k = str.length,                   // длина сообщения
        m = parseInt(key),                  // кол-во строк
        n = Math.floor((k - 1) / m) + 1;    // кол-во столбцов
    let i, j, index, decodeArr = [];

    if (decode) {
        for (i = 0, j = 0; i < k; i++) {
            index = Math.floor((m * (i % n)) + (i / n));

            if (index < k) {
                decodeArr[index] = str[j];
                j += 1;
            } else {
                decodeArr[index] = '.';
            }
        }
    } else {
        for (i = 0; i < m * n; i++) {
            index = Math.floor((m * (i % n)) + (i / n));
            decodeArr.push(str[index]);
        }
    }

    return decodeArr.join('');
}

const str = "Мы не выносим людей с теми же недостатками, что и у нас.";
let decode = reshuffle(str, 3);
console.log(`Decode ${decode}`);
console.log("--------------------")
console.log(`Encode ${reshuffle(decode, 3, false)}`);