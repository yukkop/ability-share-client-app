const { app, BrowserWindow } = require('electron')
const { dialog } = require('electron')


const createWindow = () => {
  const win = new BrowserWindow({
    width: 800,
    height: 600
  })

  win.loadFile('index.html')
}

app.commandLine.appendSwitch('ignore-certificate-errors')
app.whenReady().then(() => {
  console.log(123)
  createWindow()
  const { net } = require('electron')

  let data:any
  const request = net.request({
    method: "GET",
    // protocol: "http:",
    // hostname: "127.0.0.1:2999",
    url: "https://127.0.0.1:2999/liveclientdata/activeplayer/",
    
  })
  console.log(request)
  request.on('response', (response:any) => {
    console.log(response.statusCode)
    console.log(JSON.stringify(response.headers))
    response.on('data', (chunk:any) => {
        data = chunk
    })
  })
  request.end()
})
