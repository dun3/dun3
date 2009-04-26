if ( wgNamespaceNumber == 14 ) addOnloadHook ( catfood_init ) ;

function catfood_init () {
  var ptb = document.getElementById ( "p-tb" ) ;
  if ( !ptb ) return ;
  var ul = ptb.getElementsByTagName("UL")[0] ;
  if ( !ul ) return ;
  var li = document.createElement ( "LI" ) ;
  var a = document.createElement ( "A" ) ;
  a.href = "http://toolserver.org/~magnus/catfood.php?category=" + encodeURIComponent (wgTitle.split(" ").join("_")) ;
  a.appendChild ( document.createTextNode ( "Category RSS feed" ) ) ;
  li.appendChild ( a ) ;
  ul.appendChild ( li ) ;
}