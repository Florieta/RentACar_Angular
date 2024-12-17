# Rent A Car

# App Overview

The application is designed for two types of users: Dealers and Renters. It is built with a custom server developed in .NET/C#, and the front-end is powered by Angular. The app interacts with a SQL Server database and utilizes Azure Blob Storage for storing car images.

# User Types
- Dealer:

Username: User123
Password: User123!

Role: Dealers can manage the cars they add to the platform. They can add new cars, edit and delete them, view a list of cars they have added, and manage their profile information.

Features:
Search and filter cars by name, category, and other attributes.
View detailed information about each car.
Add new cars to the catalog.
View and manage a list of their cars.
Edit and delete their own cars.
Edit their profile information.
Logout.

- Renter:

Username: Renter
Password: User123!

Role: Renters can browse cars available for rent, make bookings, view their onw bookings and manage their personal details.

Features:
Search and filter cars by name, category, and other attributes.
View detailed information about each car.
Book cars they are interested in.
View and manage their bookings.
Cancel their own bookings if the start date is before today.
Edit their profile information.
Logout.

# Public Pages (Accessible without authentication):
Home Page: The landing page where users can learn about the platform and its services.
Catalog: A list of all available cars for rent, with the ability to search by car name, category, or other attributes.
Car Details: Detailed information about a car, including its features, price, and availability.
Login and Registration: Pages to log in or register as a Dealer or a Renter.

# Private Pages (Accessed after authentication):

- For Dealers:

Add Car: Dealers can add new cars to the catalog by filling in the car details (name, category, description, image, etc.).
My Cars: Dealers can view all cars they have added to the platform. They can manage or remove them if necessary.
Edir Car: Dealer can edit all cars they have added to the platform.
My Profile: Dealers can view and edit their personal details (name, contact, etc.) through an edit option.

- For Renters:

My Bookings: Renters can view a list of all their car bookings, along with the booking status and details. They also can cancel a booking.
My Profile: Renters can view and edit their personal details.

- Search and Pagination:
Catalog Page: The catalog allows users to search for cars based on name, category, and other relevant criteria. Pagination is implemented to help navigate through multiple pages of cars efficiently.

Car Details: Each car in the catalog has a "See Details" button that displays a detailed view of the car. Renters can click the "Book Now" button if they wish to rent that car.

- Authentication and Navigation:
Login: Users can log in with their credentials to access their personalized dashboard. Dealers and Renters have different navigation options based on their roles.
Logout: Both users can log out using a logout button in the navigation.

# Flow of the Application:

- Unauthenticated Users:

Visitors can browse the Home, Catalog, and Car Details pages. They can register either as a Dealer or a Renter through the login/register options.

- Authenticated Dealers:

Once logged in, Dealers have additional options:
Add Car: Add a new car to the platform.
My Cars: View all cars they have added and manage them.
My Profile: View and update their personal details.
Logout: To sign out from the system.

- Authenticated Renters:

Renters can:
My Bookings: View and manage their bookings.
My Profile: Edit their personal details.
Book Now: Renters can click the "Book Now" button on the car details page to initiate a car booking.
Logout: To sign out from the system.

# Additional Features:

- Pagination: The catalog page is paginated to manage a large number of cars, making navigation smoother and more efficient.
- Search: Option to search for cars based on name, category, and other relevant criteria on Catalog page. 
- Validation: All forms have validation.
- Angular animation: Add slider photos on Home page.
- Unit tests
