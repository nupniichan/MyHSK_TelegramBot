## MyHSK

This Telegram bot helps you get game keys for Hamster Kombat games. You can request keys for Bike, Clone, Cube, Merge, and Train games. 

**Features:**

* **Get 4 keys at a time:**  For each game, you can request up to 4 keys at once.
* **Automatic key update:**  The bot automatically updates the key list by reading keys from a local file. 
* **User-friendly commands:** Simple commands allow you to easily request keys for different games. 

**How to Use:**

1. **Get your bot's token:**  
    * You can get your bot's token from the BotFather on Telegram. 
2. **Set up your key files:**
    * Create a folder named `KeyList` in the same directory as your bot's code.
    * Create separate text files for each game, named `bike_code.txt`, `clone_code.txt`, `cube_code.txt`, `merge_code.txt`, and `train_code.txt`.
    * Populate these files with valid game keys (you can get keys from the websites listed below).
3. **Run the bot:**
    * Set your bot's token as an environment variable named `TOKEN` (or update the hardcoded value in the code).
    * Run the bot application. 
4. **Start using the bot:**
    * Open Telegram and search for your bot's username (you can find it using the `GetMeAsync()` method).
    * Use the following commands:
        * `/bike` - Get 4 keys for Bike
        * `/clone` - Get 4 keys for Clone
        * `/cube` - Get 4 keys for Cube
        * `/merge` - Get 4 keys for Merge
        * `/train` - Get 4 keys for Train
        * `/twerk` - Get 4 keys for TwerkRace

**Key Sources:**

* **Bike:**  https://hamsterkey-bike.netlify.app/
* **Clone:** https://hamsterkey-clone.netlify.app/
* **Cube:** https://hamsterkey-cube.netlify.app/
* **Merge:** https://hamsterkey-merge.netlify.app/
* **Train:** https://hamsterkey-train.netlify.app/
* **Twerk:** https://hamsterkey-twerk.netlify.app/

**Dependencies:**

* Telegram.Bot library (install using `dotnet add package Telegram.Bot`)

**To run the bot:**

1.  Clone this repository.
2.  Install dependencies: `dotnet restore`.
3.  Run the application: `dotnet run`.

*Note: This bot requires valid game keys. Please obtain keys from the provided websites and place them in their respective text files before running the bot.*
*Note: I have a script that can generate ton of keys automatically but it require a high-end pc to do that so if you want, you can DM me.*
