# Gentleman's Code Barber Studio

A complete ASP.NET Core MVC fictional barber shop website created for the Talent Forge Junior Full-Stack Developer practical assessment.

## Included
- Responsive Home, Services, About, Contact, Booking and Terms & Conditions pages
- Responsive mobile navigation and premium black/cream/gold branding
- Working SQLite/Entity Framework Core booking persistence
- Service, barber, date, time and customer detail selection
- Server-side validation and same-barber time conflict protection
- Booking confirmation page
- Dynamic Google Calendar event generation
- Dynamic Apple Calendar-compatible `.ics` download
- Purposeful first-visit popup with close behaviour
- Professional footer, social links, contact details and opening hours

## Run in Visual Studio
1. Open `GentlemansCode.csproj` in Visual Studio 2022.
2. Restore NuGet packages.
3. Press **F5** or **Ctrl+F5**.
4. Browse to Home → Services → Book Now.
5. Complete a test booking and verify both calendar options.

The database is created automatically as `gentlemanscode.db` on first run and seeded with services and barbers.

## Assessment note
The assessment's final submission requires one publicly accessible live website URL. This project is the deployable source project; publish it to an ASP.NET-compatible host before submitting the live URL.
