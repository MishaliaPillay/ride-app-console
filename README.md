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

