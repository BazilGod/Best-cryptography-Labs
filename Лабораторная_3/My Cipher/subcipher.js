"use strict"

var proto;

function SubCipher( key ) {

    this.alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    this.key = key || "XYZABCDEFGHIJKLMNOPQRSTUVW";

};

proto = SubCipher.prototype;


proto.encode = function( message ) {
    var self = this;
    var i = 0;
    var encoded = [];
    var cchar, ind, len, lcMessage;

    if( !message ) return '';

    len = message.length;

    for( ; i < len; i++) {
        cchar = message.charAt(i);
        ind = self.alphabet.indexOf(cchar.toUpperCase());
        if( ind === -1 ) {
            encoded.push( cchar );
        } else {
            encoded.push( self.key.charAt( ind ) );
        }
    }

    return encoded.join('');
}

proto.decode = function( message ) {
    var self = this;
    var i = 0;
    var decoded = [];
    var cchar, ind, len, lcMessage;

    if( !message ) return '';

    len = message.length;

    for( ; i < len; i++) {
        cchar = message.charAt(i);
        ind = self.key.indexOf(cchar.toUpperCase());

        if( ind === -1 ) {
            decoded.push( cchar );
        } else {
            decoded.push( self.alphabet.charAt( ind ) );
        }
    }

    return decoded.join('');
}



module.exports = function( key ) {

    return new SubCipher( key );

}
