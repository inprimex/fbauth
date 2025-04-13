# Facebook OAuth Login Proof of Concept (POC)

## Overview

This is a Proof of Concept (POC) ASP.NET Core web application demonstrating Facebook OAuth 2.0 authentication integration. The application showcases how to implement secure social login using Facebook's authentication service.

## Features

- Facebook OAuth 2.0 Authentication
- Secure cookie-based authentication
- User profile information display
- Error handling and logging
- Secure configuration management

## Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022, VS Code, or any .NET-compatible IDE
- Facebook Developer Account

## Project Setup

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/facebook-login-poc.git
cd facebook-login-poc
```

### 2. Facebook App Configuration

#### Create a Facebook App

1. Go to [Facebook Developers](https://developers.facebook.com/)
2. Click "Create App"
3. Select "Consumer" as the app type
4. Fill in basic information:
   - App Name
   - Contact Email
   - Category: "Authentication & Account Creation"

#### Configure App Settings

1. In the App Dashboard, go to Settings > Basic
2. Note your App ID and App Secret
3. Add a Platform (Website):
   - Site URL: `https://localhost:7067`

#### Configure Facebook Login

1. Add the Facebook Login product to your app
2. Go to Facebook Login > Settings
3. Add Authorized Redirect URIs:
   - `https://localhost:7067/signin-facebook`
   - `http://localhost:5210/signin-facebook`
4. Configure OAuth settings:
   - Client OAuth Login: ON
   - Web OAuth Login: ON
   - Enforce HTTPS: ON
   - Embedded Browser OAuth Login: OFF

### 3. Local Configuration

#### Option 1: User Secrets (Recommended for Development)

1. Initialize User Secrets:
```bash
dotnet user-secrets init
```

2. Set Facebook App Credentials:
```bash
# Replace with your actual App ID and Secret
dotnet user-secrets set "Authentication:Facebook:AppId" "YOUR_FACEBOOK_APP_ID"
dotnet user-secrets set "Authentication:Facebook:AppSecret" "YOUR_FACEBOOK_APP_SECRET"
```

#### Option 2: Environment Variables (Alternative)

```bash
# Linux/macOS
export Authentication__Facebook__AppId="YOUR_FACEBOOK_APP_ID"
export Authentication__Facebook__AppSecret="YOUR_FACEBOOK_APP_SECRET"

# Windows PowerShell
$env:Authentication__Facebook__AppId="YOUR_FACEBOOK_APP_ID"
$env:Authentication__Facebook__AppSecret="YOUR_FACEBOOK_APP_SECRET"
```

### 4. Install Dependencies

```bash
dotnet restore
```

### 5. Run the Application

```bash
dotnet run
```

Navigate to:
- HTTPS: https://localhost:7067
- HTTP: http://localhost:5210

## Authentication Flow

1. User clicks "Login with Facebook" on the home page
2. Redirected to Facebook's login page
3. User grants app permissions
4. Facebook redirects back to the application
5. Application exchanges authorization code for an access token
6. User profile information is retrieved and displayed

## Security Considerations

- User Secrets prevent exposing credentials in source control
- HTTPS enforced for all communications
- Tokens stored in HTTP-only, secure cookies
- Minimal Facebook permissions requested
- Redirect URI validation to prevent open redirect attacks

## Project Structure

```
.
├── Controllers/
│   ├── AccountController.cs    # Handles authentication logic
│   └── HomeController.cs       # Manages home and profile pages
├── Views/                      # Razor views
│   ├── Home/
│   │   ├── Index.cshtml        # Landing page
│   │   └── Profile.cshtml      # User profile display
│   └── Shared/
│       ├── _Layout.cshtml      # Main layout
│       └── Error.cshtml        # Error handling view
├── wwwroot/                    # Static files
│   ├── css/site.css            # Styles
│   └── js/site.js              # Client-side scripts
├── Program.cs                  # Application startup
└── appsettings.json            # Configuration settings
```

## Troubleshooting

### Common Issues

1. **Authentication Failure**
   - Verify App ID and Secret are correct
   - Check redirect URIs match exactly
   - Ensure app is in development mode

2. **HTTPS Certificate**
   ```bash
   dotnet dev-certs https --trust
   ```

3. **Facebook Error Messages**
   - "URL Blocked": Check app domain settings
   - "Invalid Redirect URI": Verify URIs precisely
   - "App Not Set Up": Ensure app is configured for test users

## Extending the POC

Potential improvements:
- Implement user registration database
- Add more authentication providers
- Enhance error handling
- Improve UI/UX
- Add rate limiting
- Implement more robust logging

## License

[MIT License](LICENSE)

## Contributing

Contributions are welcome! Please submit pull requests or open issues.
