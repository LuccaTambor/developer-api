const PROXY_CONFIG = [
  {
    context: [
      '/api'
    ],
    target: 'https://localhost:7181/',
    secure: false,
    changeOrigin: false,
    pathRewrite: {
      "^/": ""
    }
  }
]

module.exports = PROXY_CONFIG;