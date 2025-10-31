export const ENDPOINTS = {
  "AUTH": {
    "SIGNUP": "/api/auth/signup",
    "SIGNIN": "/api/auth/signin",
  },
  "OBSERVING": {
    "GET": "/api/observings/",
    "GET_ALL": "/api/observings",
    "CREATE": "/api/observings",
    "EDIT": "/api/observings/",
    "DELETE": "/api/observings/",
    "ENTRIES": "/api/observings/:id/entries",
    "ENTRY_PAYLOAD": "/api/observings/:observingId/entries/:entryId/payload",
    "ENTRY_DIFF_PAYLOAD": "/api/observings/:observingId/entries/:entryId/diff",
  },
  "TEMPLATE": {
    "GET_ALL": "/api/templates"
  },
  "HANGFIRE": {
    "DASHBOARD": "/api/hangfire"
  }
} as const;