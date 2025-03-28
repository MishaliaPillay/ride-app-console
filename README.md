# Ride Console App

A Ride-Sharing System 

## Overview

This platform allows users to sign up as Drivears or passengers. Passengers are allowed to request rides and this can be accepted by Drivers. 


This platform allows fitness professionals to create accounts, manage their clients, and develop customized meal plans using a curated ingredient database. Trainers can sign up, add client accounts, build an ingredient library, and recommend personalized meals to their clients.

## Features

- **Driver Accounts**: Sign Up, Accept rides, Complete Rides
- **Passenger**: Sign Up, Request rides, View Ride History, Rate drivers 


## Tech Stack

- **C# Console App**: OOP and LinQ


## Getting Started

### Prerequisites

- Visual Studio
- Git

### Installation

1.  Clone the repository:

    ```bash
    git clone https://github.com/MishaliaPillay/ride-app-console.git

    ```

2.  Install dependencies:

    ```bash
    Install-Package Newtonsoft.Json

    ```


4.  Run the App:

     Either open the build file or Run in Visual Studio


## Project Structure

```
ride-app-console/

+---Application
|   +---Interfacse
|   |       IAdmin.cs
|   |       IDriver.cs
|   |       IPassenger.cs
|   |       IRide.cs
|   |       IUser.cs
|   |       
+---Data
|       data.json
|       Drivers.json
|       Passengers.json
|       rides.json
|       
+---Enities
|       Admin.cs
|       Driver.cs
|       Location.cs
|       Passenger.cs
|       Ride.cs
|       User.cs
|       
+---Infrastructure
|   +---Repositories
|   |       DriverRepository.cs
|   |       PassengerRepository.cs
|   |       RideRepository.cs
|   |       UserRepository.cs
|   |       
|   \---Services
|           RideController.cs
|           UserService.cs
|           
\---Presentation
    +---Controllers
    |       UserController.cs
    |       
    \---Menus
            DriverMenu.cs
            MainMenu.cs
            PassengerMenu.cs
|   Program.cs

```

## Details on the project

- Pythagoras Theorem to calculate distances.
- Data is stored in JSON files 
     -  Driver and Passenger Data saved to data.json
     -  Rides Stored in rides.json
-Structure to follow for adding in functionality
   ```bash
          Menu 
            |
      UserController
        /           \
     UserService  -- RideController
         /     \        /
     UserRepo  Ride Repo
    ```
--

## Screens in the app
### Validation Login Page
![image](https://github.com/user-attachments/assets/ea98e068-4ab5-4599-8605-7f49c5580f50)

### User Registration
 ![image](https://github.com/user-attachments/assets/e6dd8555-4ecb-496f-be2d-7b5a569b5e38)

### Driver Dashboard
![image](https://github.com/user-attachments/assets/a302289c-dad2-4fff-9d38-fc3f9f4ea480)

### Driver View Available rides 
1![image](https://github.com/user-attachments/assets/a81f4055-bacc-436e-9526-abd7eda24da3)

### Passenger Dashboard
![image](https://github.com/user-attachments/assets/8ccbd8e7-7e00-40f5-ba8f-6460315dee20)

### Passenger Request Ride
1![image](https://github.com/user-attachments/assets/4b59c56b-a7ce-4457-b151-4a6c46f2481b)

### Passenger Views ride history
![image](https://github.com/user-attachments/assets/21890572-663a-4ba7-b9e4-3590a15c42de)


