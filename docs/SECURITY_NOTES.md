# 🔐 Security Notes

## Authentication
- JWT-based authentication will be implemented.

## Authorization
- Role-based access control (`GlobalRole`, `TaskRole`, etc.)
- Granular per-project permissions planned

## Data Protection
- Passwords are hashed with salt (currently mocked, real hashing to be added)
- Sensitive fields are private-set with proper encapsulation

## TODO:
- Enable HTTPS
- Add authentication middleware
- Protect endpoints with `[Authorize]` attributes
