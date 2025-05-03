# 🚀 Deployment Guide

## Prerequisites
- .NET 8 SDK
- PostgreSQL
- Docker (planned)
- EF Core CLI tools

## Local Setup
```bash
git clone https://github.com/<username>/ProjectPulse.Backend.git
cd ProjectPulse.Backend
dotnet build
dotnet ef database update
dotnet run
