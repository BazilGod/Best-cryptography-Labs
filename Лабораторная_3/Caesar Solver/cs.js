var person = prompt("Please enter your text.", "abc.");
var caesarShift = function(person, amount) {
  if (amount < 0) {
    return caesarShift(person, amount + 26);
  }
  var output = '';
  
  for (var i = 0; i < person.length; i ++) {
    var c = person[i];
    if (c.match(/[a-z]/i)) {
      var code = person.charCodeAt(i);
      
      if ((code >= 65) && (code <= 90))
      c = String.fromCharCode(((code - 65 + amount) % 26) + 65);
      
      else if ((code >= 97) && (code <= 122))
      c = String.fromCharCode(((code - 97 + amount) % 26) + 97);
    }
    output += c;
  }
  return output;

};
