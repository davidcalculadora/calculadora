var http = require('http');
var url = require('url');
var querystring = require('querystring');
var fs = require('fs');


// En js no hay constantes, así que usaremos variables normales pero con
// mayúsculas, para dejar claro su cometido
var PORT = 8000;


// Creamos un objeto "http server" de Node, y le decimos que empiece a
// escuchar en el puerto PORT.
// Nos enviará un evento 'listen' cuando efectivamente ya esté escuchando
// peticiones. Nos enviará un evento 'request' cuando llegue una petición
// desde un navegador.
var server = http.createServer();
server.listen(PORT);
server.on('listening', onListen);
server.on('error', onError);
server.on('request', onRequest);


// Recepción del evento 'listen'. Simplemente lo pintamos en pantalla
function onListen() {
  console.log('Listening on port ' + PORT);
}


// Errores en el servidr. No hacemos nada, más que pintarlo.
function onError(err) {
  console.log('Http error: ' + err.message);
}


// Recepción de un evento 'request'. Aquí está el meollo del servidor.
// Tenemos 2 parámetros: 'request', de donde podemos averiguar qué está
// pidiendo el navegador, y 'response', donde rellenaremos qué le queremos
// devolver al navegador
function onRequest(request, response) {
  console.log('');
  console.log('Request received');
  console.log('   url: ' + request.url);
  var pathname = url.parse(request.url).pathname;
  console.log('   pathname: ' + pathname);

  // En función del pathname, devolvemos una cosa u otra
  switch (pathname) {

    // Cuando hay que devolver un fichero ...
    case '/':
    case '/main':
    case '/main.html':
    case '/index':
    case '/index.html':
      processFile(response, 200, 'text/html', './static/html/main.html');
      break;
    case '/main.css':
      processFile(response, 200, 'text/css', './static/css/main.css');
      break;
    case '/main.js':
      processFile(response, 200, 'text/javascript', './static/javascript/main.js');
      break;
    case '/jquery.js':
      processFile(response, 200, 'text/javascript', './static/javascript/jquery.js');
      break;
      
    // Otros casos:

    // Algunos navegadores piden icono para su pestaña. No hago nada,
    // envío una respuesta vacía al navegador
    case '/favicon':
      response.end()
      break;

    // Nos han pedido algo que no teníamos previsto. Retornamos un
    // HTML de error.
    default:
      processFile(response, 404, 'text/html', './static/html/error.html');
      break;
  }
}


// Con esta función pretendemos atender peticiones que nos están pidiendo
// un fichero (html, js, css, png ...).
function processFile(response, result, type, filepath) {
  readFile(filepath, function(content) {
    console.log('Returning file ' + filepath)
    response.writeHead(result, {'Content-type': type});
    response.write(content);
    response.end()
  })
}


// Lee un fichero, y retorna su contenido (vía callback).
// Además vamos a hacer una mejora: cuando nos pidan un fichero y lo leamos
// de disco, nos guardamos el contenido EN MEMORIA... y así la próxima vez
// que nos lo pidan, no hará falta acceder a disco
// UNA COSA QUE ESTOY HACIENDO MAL: no estoy comprobando errores (p.e., que
// el fichero no exista), y la función de callback "cb" no está retornando
// los posibles errores.
var cache = {}
function readFile(filepath, cb) {
  var content = cache[filepath];
  if (content == undefined) {
    fs.readFile(filepath, 'utf8', function(err, data) {
      if (err == null) {
        console.log('Reading file ' + filepath)
        content = data;
        cache[filepath] = content;
        cb(content);
      }
      else {
        console.log('readFile error: ' + err.message);
        throw err; // Aquí debería hacer algo más sofisticado, claro
      }
    })
  }
  else {
    console.log('Retrieving cached file ' + filepath)
    cb(content);
  }
}