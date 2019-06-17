function process(action){
	// извлекаем ключи из полей ввода
	var colsKey = document.getElementById('colsKey').value;
	var rowsKey = document.getElementById('rowsKey').value;
	
	var inputMessage = document.getElementById('inputMessage').value; // извлекаем входное сообщение

	inputMessage = inputMessage.replace(/\s/g,'').toUpperCase(); // убираем пробелы и делаем все буквы прописными
	// дополняем входное сообщение знаками подчёркивания если оно короче чем нужно
	for (var i = inputMessage.length; i <colsKey.length * rowsKey.length; i++) {
		inputMessage += '_';
	};
	var cryptResult = crypt(inputMessage, colsKey, rowsKey); // шифруем / дешифруем
	// формируем строку с результатом
	var regExp = new RegExp('.{'+ colsKey.length +'}','g');
	var resultLine = cryptResult.right.join('').replace(regExp,'$& ');
	// формируем таблички
	var leftTableView = '';
	var middleTableView = '';
	var rightTableView = '';
	for(var j =0;j<rowsKey.length; j++){
		// вырезаем из начала массива одну строку и оборачиваем каждую букву в теги <td></td>
		// а строку в свою очередь в <tr></tr>
		leftTableView += '<tr>' + cryptResult.left.splice(0,colsKey.length).join('').replace(/./g,'<td>$&</td>') + '</tr>'
		middleTableView += '<tr>' + cryptResult.middle.splice(0,colsKey.length).join('').replace(/./g,'<td>$&</td>') + '</tr>'
		rightTableView += '<tr>' + cryptResult.right.splice(0,colsKey.length).join('').replace(/./g,'<td>$&</td>') + '</tr>';

	}
	leftTableView = '<table><caption>Исходное сообщение</caption>' + leftTableView + '</table>';
	middleTableView = '<table><caption>Перестановка строк</caption>' + middleTableView + '</table>';
	rightTableView = '<table><caption>Перестановка столбцов</caption>' + rightTableView + '</table>';
	
	// выводим результат
	document.getElementById('output').innerHTML = leftTableView + middleTableView + rightTableView;	
	document.getElementById('outputLine').innerText = ((action=='encrypt')?'Шифровка':'Дешифровка')+': '+resultLine;
	
}

//функция шифрования/дешифровки
function crypt(message, colsKey, rowsKey){
	var result = []; // массив для выходного сообщения
	var middle = []; // массив для средней таблицы
	var left = []; // и для левой
	// узнаём размерность таблицы
	var colsCount = colsKey.length; // количество столбцов
	var rowsCount = rowsKey.length; // количество строк
	// манипуляции с буквами (работаем с одномерными массивами потому тут магия с индексами)
	for(var row = 0; row<rowsCount; row++) {// бежим по строкам
		for(var col = 0; col<colsCount; col++){ // и столбцам
 			// новые координаты буквы
 			var newCol = colsKey[col]-1;
 			var newRow = rowsKey[row]-1;
 			left[row*colsCount + col] = message[row*colsCount + col];
 			middle[row*colsCount + newCol] = message[row*colsCount + col];
 			result[newRow*colsCount + newCol] = message[row*colsCount + col];
		}
	}
	return {left: left, middle: middle, right: result };
}
