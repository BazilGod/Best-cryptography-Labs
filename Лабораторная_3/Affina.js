

function affineCrypt(text, a, b){
    text = text.toUpperCase();
    let rc = '';
    for(let i = 0; i < text.length; i++){
        let ind = (((a * (text.charCodeAt(i) - 65)) + b) % 26) + 65;
        rc += String.fromCharCode(ind);
    }
    return rc;
}

console.log('---------- task 3 ----------');
console.log(affineCrypt('Bazil', 3, 4));