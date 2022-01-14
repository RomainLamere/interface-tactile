const {WebSocketServer} = require('ws');

const wss = new WebSocketServer({ port: 3000 });
console.log('Listening on port 3000');

wss.on('connection', (ws) => {
    console.log('New Connection')
    ws.send('C tres la connexion')
    ws.onmessage = (e) => {
        console.log(e.data)
    }
});