# C# Weather API

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A small, minimal .NET API that returns current weather for a city using the [Visual Crossing API](https://www.visualcrossing.com/weather-api/).

Quick start
-----------
Prerequisites: 
- .NET 10 SDK 
- Visual Crossing API key.

```bash
http://localhost:5000/weather/London
```
Output:
```json
{
  "city": "london",
  "temperature": -1,
  "condition": "Clear"
}
```

What’s inside
--------------
- `Program.cs` - API entry and DI wiring
- `Clients/` - external API client
- `Services/` - business logic
- `Models/` - returned DTOs

Architecture
---------------------
[HTTP client] → [Minimal API] → [WeatherService] → [VisualCrossingClient] → [Visual Crossing API]

Notes
-----
- Used user-secrets for local dev; 
- 

License
-------
This project is released under the MIT License - see the `LICENSE` file.
