export const ENDPOINTS = {
  "AUTH": {
    "SIGNUP": "/api/auth/signup",
    "SIGNIN": "/api/auth/signin",
  },
  "OBSERVING": {
    "GET_ALL": "/api/observings",
    "CREATE": "/api/observings",
    "EDIT": "/api/observings/",
    "DELETE": "/api/observings/"
  },
  "TEMPLATE": {
    "GET_ALL": "/api/templates"
  }
} as const;