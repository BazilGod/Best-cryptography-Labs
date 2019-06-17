//lucifer.js
//Coded by Miles07 (Michael A. West), v1.0r (c) 27 Mar 2016 (UTC)
//A HTML/Javascript/jQuery page based on the C implementation of the IBM Lucifer cipher
//Acknowledgments to Arthur Sorkin and Jonathan M. Smith for sources

var luciferjs = {
  encipher: false,
  decipher: true,
  message: [],
  key: [],
  diffusion: [ 7, 6, 2, 1, 5, 0, 3, 4 ],
  iFixedPerm: [ 2, 5, 4, 0, 3, 1, 7, 6 ],
  sZero: [ 12, 15, 7, 10, 14, 13, 11, 0, 2, 6, 3, 1, 9, 4, 5, 8 ],
  sOne: [ 7, 2, 14, 9, 3, 11, 0, 4, 12, 13, 1, 10, 6, 15, 8, 5 ],
  
  run: function(direction) {
    
    var tcbI, tcbC = (direction == this.decipher) ? 8 : 0, 
      hi, lo, h_0 = 0, h_1 = 1;
    
    for ( var round = 0; round < 16; round++ ) {
      // console.log('round', round, '>> tcbC', tcbC, '/', tcbC.toString(16));
      var report = tcbC;
      
      tcbC = ( direction == this.decipher ) ? ( tcbC + 1 ) & 0xf : tcbC;
      tcbI = tcbC;
      
      for ( var b = 0; b < 8; b++ ) {
        // if(round < 3) console.log('> byte', b, '>>', this.message[ ( h_1 * 64 ) + ( 8 * b ) + 3 ], this.message[ ( h_1 * 64 ) + ( 8 * b ) + 2 ], this.message[ ( h_1 * 64 ) + ( 8 * b ) + 1 ], this.message[ ( h_1 * 64 ) + ( 8 * b ) + 0 ], this.message[ ( h_1 * 64 ) + ( 8 * b ) + 7 ], this.message[ ( h_1 * 64 ) + ( 8 * b ) + 6 ], this.message[ ( h_1 * 64 ) + ( 8 * b ) + 5 ], this.message[ ( h_1 * 64 ) + ( 8 * b ) + 4 ]);
        lo = 
            this.message[ ( h_1 * 64 ) + ( 8 * b ) + 7 ] * 8
          + this.message[ ( h_1 * 64 ) + ( 8 * b ) + 6 ] * 4
          + this.message[ ( h_1 * 64 ) + ( 8 * b ) + 5 ] * 2
          + this.message[ ( h_1 * 64 ) + ( 8 * b ) + 4 ];
        hi = 
            this.message[ ( h_1 * 64 ) + ( 8 * b ) + 3 ] * 8
          + this.message[ ( h_1 * 64 ) + ( 8 * b ) + 2 ] * 4
          + this.message[ ( h_1 * 64 ) + ( 8 * b ) + 1 ] * 2
          + this.message[ ( h_1 * 64 ) + ( 8 * b )     ];
        
        // if(round < 3) console.log('sZero[',lo,']', this.sZero[ lo ]);
        // if(round < 3) console.log('sZero[',hi,']', this.sZero[ hi ]);
        // if(round < 3) console.log('sOne[',lo,']', this.sOne[ lo ]);
        // if(round < 3) console.log('sOne[',hi,']', this.sOne[ hi ]);
        // if(round < 3) console.log('key[ ( 8 * tcbI ) + b ]', b, tcbI, tcbI * 8, (tcbI * 8) + b, this.key[ ( 8 * tcbI ) + b ], 1 - this.key[ ( 8 * tcbI ) + b ]);
        // if(round < 3) console.log('first', this.sZero[ lo ], '+ ( 16 x', this.sOne[ hi ], '=', 16 * this.sOne[ hi ], ') =', ( this.sZero[ lo ] + 16 * this.sOne[ hi ] ));
        // if(round < 3) console.log('  ', ( this.sZero[ lo ] + 16 * this.sOne[ hi ] ),'x',( 1 - this.key[ ( 8 * tcbI ) + b ]),'=',( this.sZero[ lo ] + 16 * this.sOne[ hi ] ) * ( 1 - this.key[ ( 8 * tcbI ) + b ]));
        // if(round < 3) console.log('last', this.sZero[ hi ], '+ ( 16 x', this.sOne[ lo ], '=', 16 * this.sOne[ lo ], ') =', ( this.sZero[ hi ] + 16 * this.sOne[ lo ] ));
        // if(round < 3) console.log('  ', ( this.sZero[ hi ] + 16 * this.sOne[ lo ] ),'x',this.key[ ( 8 * tcbI ) + b ],'=',( this.sZero[ hi ] + 16 * this.sOne[ lo ] ) * this.key[ ( 8 * tcbI ) + b ]);
        
        var v = 
            ( this.sZero[ lo ] + 16 * this.sOne[ hi ] ) * ( 1 - this.key[ ( 8 * tcbI ) + b ])
          + ( this.sZero[ hi ] + 16 * this.sOne[ lo ] ) *       this.key[ ( 8 * tcbI ) + b ];
        
        // if(round < 3) console.log('>>>>> v', v);
        
        var tr = [ ];
        
        for ( var j = 0; j < 8; j++ ) {
          tr[ j ] = v & 0x1;
          v = v >> 1;
        }
        
        // if(round < 3) console.log('>>>>> new v', v);
        // if(round < 3) console.log('tr', tr); //=v in binary, reverse
        
        for ( var k = 0; k < 8; k++ ) {
          var index = ( this.diffusion[ k ] + b ) & 0x7,
              ciphered = this.message[ ( h_0 * 64 ) +  ( 8 * index ) + k   ]
                       + this.key    [ ( 8 * tcbC ) + this.iFixedPerm[ k ] ]
                       +      tr     [                this.iFixedPerm[ k ] ]
          ;
          // if(round < 1) console.log('attempt',k);
          // if(round < 1) console.log('index:','diffusion[',k,'] =',this.diffusion[ k ],'; b =',b,'; d[k]+b = ',this.diffusion[ k ] + b,', masked as',( this.diffusion[ k ] + b ) & 0x7);
          // if(round < 1) console.log('message:',h_0,h_0*64,'+',8*index,'+',k,'=',( h_0 * 64 ) + ( 8 * index ) + k,'; message thereof =',this.message[ ( h_0 * 64 ) + ( 8 * index ) + k ]);
          // if(round < 1) console.log('key:',tcbC,8*tcbC,'+',iFixedPerm[k],'=',( 8 * tcbC ) + iFixedPerm[ k ],'; key thereof =',this.key[ ( 8 * tcbC ) + iFixedPerm[ k ] ]);
          // if(round < 1) console.log('tr:',tr[iFixedPerm[ k ] ],'; ciphered:',ciphered,', to replace message[',( h_0 * 64 ) + ( 8 * index ) + k,']');
          this.message[ ( h_0 * 64 ) + ( 8 * index ) + k ] = ciphered & 0x1;
        }
        
        tcbC = ( b < 7 || direction == this.decipher ) ? ( tcbC + 1 ) & 0xf : tcbC;
        // if(round < 3) console.log('tcbC at end', tcbC, '/', tcbC.toString(16));
        // if(round < 3) console.log('-------------------------------------------------------------------------------');
        report += ' ' + tcbC;
      }
      
      var swapper = h_0;
      h_0 = h_1;
      h_1 = swapper;
      
      //console.log('round', round, ':', report);
    }
    
    for ( var x = 0; x < 8; x++ ) {
      for ( var y = 0; y < 8; y++ ) {
        var offset = ( 8 * x ) + y,
            t = this.message[ offset ];
        this.message[ offset ] = this.message[ 64 + offset ];
        this.message[ 64 + offset ] = t;
      }
    }
  },
  
  keyConversion: function(key_pt) {
    this.key = [ ];
    
    for ( var i = 0; i < 32; i+=2 ) {
      var bin_charsHi = parseInt( key_pt[   i   ], 16 ).toString( 2 ).split( "" ),
          bin_charsLo = parseInt( key_pt[ i + 1 ], 16 ).toString( 2 ).split( "" );
      
      for ( var k = 0; k < 4; k++ ) {
        if ( bin_charsHi[ k ] != '0' && bin_charsHi[ k ] != '1' ) {
          bin_charsHi.unshift( 0 );
        }
        bin_charsHi[ k ] = parseInt( bin_charsHi[ k ], 2 );
        
        if ( bin_charsLo[ k ] != '0' && bin_charsLo[ k ] != '1' ) {
          bin_charsLo.unshift( 0 );
        }
        bin_charsLo[ k ] = parseInt( bin_charsLo[ k ], 2 );
      }
      
      bin_charsHi.reverse();
      bin_charsLo.reverse();
      this.key = this.key.concat( bin_charsLo, bin_charsHi );
    }
    
    //console.log('key', this.key);
  },
  
  msgConversion: function(msg_pt) {
    this.message = [ ];
    
    for ( var i = 0; i < 32; i+=2 ) {
      var bin_charsHi = parseInt( msg_pt[   i   ], 16 ).toString( 2 ).split( "" ),
          bin_charsLo = parseInt( msg_pt[ i + 1 ], 16 ).toString( 2 ).split( "" );
      for ( var k = 0; k < 4; k++ ) {
        if ( bin_charsHi[ k ] != '0' && bin_charsHi[ k ] != '1' ) {
          bin_charsHi.unshift( 0 );
        }
        bin_charsHi[ k ] = parseInt( bin_charsHi[ k ], 2 );
        
        if ( bin_charsLo[ k ] != '0' && bin_charsLo[ k ] != '1' ) {
          bin_charsLo.unshift( 0 );
        }
        bin_charsLo[ k ] = parseInt( bin_charsLo[ k ], 2 );
      }
      
      bin_charsHi.reverse();
      bin_charsLo.reverse();
      this.message = this.message.concat( bin_charsLo, bin_charsHi );
    }
  },
  
  keyReversion: function() {
    var response = '';
    
      for ( var c = 0; c < 32; c += 2 ) {
        //console.log('key range', ( c * 4 ), ' - ', ( ( c + 1) * 4 ) + 3);
        
        var hi = this.key [ ( ( c + 1) * 4 ) + 3 ] * 8
               + this.key [ ( ( c + 1) * 4 ) + 2 ] * 4
               + this.key [ ( ( c + 1) * 4 ) + 1 ] * 2
               + this.key [ ( ( c + 1) * 4 ) + 0 ],
            lo = this.key [ ( ( c + 0) * 4 ) + 3 ] * 8
               + this.key [ ( ( c + 0) * 4 ) + 2 ] * 4
               + this.key [ ( ( c + 0) * 4 ) + 1 ] * 2
               + this.key [ ( ( c + 0) * 4 ) + 0 ];
        
        //console.log('hi',this.key [ ( ( c + 0) * 4 ) + 3 ],this.key [ ( ( c + 0) * 4 ) + 2 ],this.key [ ( ( c + 0) * 4 ) + 1 ],this.key [ ( ( c + 0) * 4 ) + 0 ],'=',hi.toString(16));
        //console.log('lo',this.key [ ( ( c + 1) * 4 ) + 3 ],this.key [ ( ( c + 1) * 4 ) + 2 ],this.key [ ( ( c + 1) * 4 ) + 1 ],this.key [ ( ( c + 1) * 4 ) + 0 ],'=',lo.toString(16));
        
        var output = hi.toString(16) + lo.toString(16);
        
        //console.log('output', output);
        
        response += output;
      }
    //console.log('response', response.toString(16));
    
    return response;
  },
  
  msgReversion: function() {
    var response = '';
    
      for ( var c = 0; c < 32; c += 2 ) {
        var hi = this.message [ ( ( c + 1) * 4 ) + 3 ] * 8
               + this.message [ ( ( c + 1) * 4 ) + 2 ] * 4
               + this.message [ ( ( c + 1) * 4 ) + 1 ] * 2
               + this.message [ ( ( c + 1) * 4 ) + 0 ],
            lo = this.message [ ( ( c + 0) * 4 ) + 3 ] * 8
               + this.message [ ( ( c + 0) * 4 ) + 2 ] * 4
               + this.message [ ( ( c + 0) * 4 ) + 1 ] * 2
               + this.message [ ( ( c + 0) * 4 ) + 0 ];
               
      var output = hi.toString(16) + lo.toString(16);
      
      response += output;
    }
    return response;
  }
};