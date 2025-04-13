#!/bin/bash

# Facebook Login POC Setup Script

# Color codes
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Function to print status messages
print_status() {
    if [ "$1" = "success" ]; then
        echo -e "${GREEN}✔ $2${NC}"
    elif [ "$1" = "warning" ]; then
        echo -e "${YELLOW}⚠ $2${NC}"
    else
        echo "$2"
    fi
}

# Check .NET SDK
print_status "info" "Checking .NET SDK..."
dotnet --version || { 
    print_status "warning" ".NET SDK not found. Please install .NET 8.0 SDK."
    exit 1
}

# Create project directory
print_status "info" "Creating Facebook Login POC project..."
mkdir -p FacebookLoginPOC
cd FacebookLoginPOC

# Create new web project
dotnet new web -f net8.0
dotnet new gitignore

# Add required packages
print_status "info" "Adding authentication packages..."
dotnet add package Microsoft.AspNetCore.Authentication.Facebook
dotnet add package Microsoft.AspNetCore.Authentication.Cookies

# Initialize user secrets
dotnet user-secrets init

print_status "success" "Project setup complete!"
echo ""
print_status "warning" "Next steps:"
echo "1. Configure Facebook App credentials using dotnet user-secrets"
echo "2. Add controllers and views as described in README"
echo "3. Run the project: dotnet run"
