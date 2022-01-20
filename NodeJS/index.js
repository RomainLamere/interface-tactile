const { createServer } = require("http");
const { Server } = require("socket.io");
const fs = require("fs");

const httpServer = createServer((req, res) => {
    fs.readFile(__dirname + '/ui/client.html', (err, data) =>{
        res.writeHead(200);
        res.end(data);
    })
});

const io = new Server(httpServer, {
    cors: {
        origin: "http://localhost:8080",
        credentials: true
    },
    allowEIO3: true
});

io.on("connection", (socket) => {
    socket.emit("toNodeUI", "Hello Node UI");

    /**
     * Event sent from the UI for this server
     * data type : {
     *      destination: string -> the name of the event to emit with the message
     *      message: string/object (to be defined) -> the data to send with the event
     * }
     */
    socket.on("fromNodeUI", (data) => {
        console.log(`Received data from UI: ${JSON.stringify(data)}`);
        io.emit(data.destination, data.message);
    });

    socket.on("eventFromScreen", (data) => {
        console.log(`Message from screen : ${data}`);
    });

    socket.on("fromUnity", (data) => {
        console.log(`Message from unity : ${data}`);
        io.emit('eventToScreen',data);
    });


    console.log(`Client connected - ${socket.id}`);
});


httpServer.listen(3000);
console.log('Listening on port 3000');