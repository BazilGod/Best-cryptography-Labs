const fs = require('fs');
let text = fs.readFileSync('./text2.txt');
const bitText = fs.readFileSync('./text4.txt');
const text5 = fs.readFileSync('./text6.txt');
const regExpSimbols = /[ |0-9|,|;|:|\/|'|.|~|"|(|)|=|-|—|^|?|*|&|%|$|#|!|@|+|\||<|>|\\|\r|\n|\t]/g;
const alfabhetENG = 'abcdefghigklmnopqrstuvwxyz';
const binAlf = '01';
let countZero = 0;
let ownNameString = "";

let hartley = function(n) {
  return Math.log2(n);
}

let shanon = function(str, alfabhet) {
  let I = 0;
  for(let i = 0; i < alfabhet.length; i++) {    
    let reg = new RegExp(alfabhet.charAt(i), 'g'),
        symbol = alfabhet.charAt(i), 
        p = (str.match(reg) === null) ? 0 : str.match(reg).length / str.length;
    
    console.log(`символ: '${symbol}', p(${symbol}) = ${p}`);
    if(p !== 0) {
      I += p * Math.log2(p);
    } 
  }

  return -I;
}

let shanon2 = function(str, alfabhet) {
  let I = 0;
  for(let i = 0; i < alfabhet.length; i++) {    
    let reg = new RegExp(alfabhet.charAt(i), 'g'),
        symbol = alfabhet.charAt(i), 
        p = (str.match(reg) === null) ? 0 : str.match(reg).length / str.length;
    if(p !== 0) {
      I += p * Math.log2(p);
    } 
  }

  return -I;
}


let shanonInfoAmount = function (str, alfabhet) {
    return str.length * shanon(str, alfabhet);
}


let hartleyInfoAmount = function (str, alfabhet) {
    return str.length * hartley(alfabhet.length);
}


let vasiliyShanon = function (name, str, alfabhet) {
  return name.length * shanon2(str, alfabhet);
}

let va3 = function (name) {
  return name.length ;
}

let vasiliyHartley = function (name,alfabhet) {
  return name.length * hartley(alfabhet.length);
}

let lastTask = function(someNumber) {
  const p = someNumber;
  const q = 1 - p;

  return (1 + p * Math.log2(p) + q * Math.log2(q)) || 0;
}


text = text.toString().toLowerCase().replace(regExpSimbols, '');
const str = 'Ikonov Vasilii Sergeevich ';
const str1 = 'Ikonov Vasilii Sergeevich '.toLowerCase().replace(regExpSimbols, '');
console.log("-----------------Задание 1 --------------")
console.log(`Длина текста = ${text.length}`);
console.log(`Энтропия по Шеннону: ${shanon(text, alfabhetENG)}`);
console.log(`Энтропия по Хартли: ${hartley(alfabhetENG.length)}`);


console.log('----------------------------');



console.log("-----------------Задание 2 --------------")
console.log('Энтропия бинарного алфавита:', shanon(bitText.toString(), binAlf));


console.log("-----------------Задание 3 --------------")

console.log(`Ikonov Vasilii Sergeevich  Количество информации(по Шеннону): ${vasiliyShanon(str1, text, alfabhetENG)}`);
console.log(`Ikonov Vasilii Sergeevich  Количество информации(по Хартли): ${vasiliyHartley(str1, alfabhetENG)}`);
console.log(`Ikonov Vasilii Sergeevich  Количество информации(по в бинарном виде): ${vasiliyShanon(text5, bitText.toString(), binAlf)}`);



console.log("-----------------Задание 4 --------------")
console.log("ФИО при 0,1", lastTask(0.1) * str1.length)
console.log("ФИО при 0,2", lastTask(0.2) * str1.length)
console.log("ФИО при 0,3", lastTask(0.3) * str1.length)
console.log("ФИО при 0,4", lastTask(0.4) * str1.length)
console.log("ФИО при 0,5", lastTask(0.5) * str1.length)
console.log("ФИО при 0,6", lastTask(0.6) * str1.length)
console.log("ФИО при 0,7", lastTask(0.7) * str1.length)
console.log("ФИО при 0,8", lastTask(0.8) * str1.length)
console.log("ФИО при 0,9", lastTask(0.9) * str1.length)
console.log("ФИО при 1", lastTask(1.0) * str1.length)