﻿// See https://aka.ms/new-console-template for more information

using System.Security.AccessControl;

Console.WriteLine("Hello, World!");

/* Q1 : THE FISCAL CODE

Each person in Italy has an unique identifying ID code issued by the national tax office after the birth registration: the Fiscal Code (Codice Fiscale). Check the Resources tab for more info on this.

Given an object containing the personal data of a person (name, surname, gender and date of birth) return the 11 code characters as a string following these steps:

Generate 3 capital letters from the surname, if it has:

At least 3 consonants then the first three consonants are used. (Newman -> NWM).
Less than 3 consonants then vowels will replace missing characters in the same order they appear (Fox -> FXO | Hope -> HPO).
Less than three letters then "X" will take the third slot after the consonant and the vowel (Yu -> YUX).
Generate 3 capital letters from the name, if it has:

Exactly 3 consonants then consonants are used in the order they appear (Matt -> MTT).
More than 3 consonants then first, third and fourth consonant are used (Samantha -> SNT | Thomas -> TMS).
Less than 3 consonants then vowels will replace missing characters in the same order they appear (Bob -> BBO | Paula -> PLA).
Less than three letters then "X" will take the the third slot after the consonant and the vowel (Al -> LAX).
Generate 2 numbers, 1 letter and 2 numbers from date of birth and gender:

Take the last two digits of the year of birth (1985 -> 85).
Generate a letter corresponding to the month of birth (January -> A | December -> T) using the table for conversion included in the code.
For males take the day of birth adding one zero at the start if is less than 10 (any 9th day -> 09 | any 20th day -> 20).
For females take the day of birth and sum 40 to it (any 9th day -> 49 | any 20th day -> 60).

*/

//Initialize Objects
List<FiscalProfile> profiles = new List<FiscalProfile>();
profiles.Add(new FiscalProfile("Matt", "Edabit", 'M', "1/1/1990"));
profiles.Add(new FiscalProfile("Helen", "Yu", 'F', "1/12/1950"));
profiles.Add(new FiscalProfile("Micky", "Mouse", 'M', "16/12/1928"));

// Get 
foreach (FiscalProfile profile in profiles)
{
    Console.WriteLine($"{profile.Firstname} {profile.Lastname} : {profile.GetFiscalCode()}");
}
Console.WriteLine($"");

// Stuff
public class FiscalProfile
{
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public char Sex { get; private set; }
    public string DateOfBirth { get; private set; }

    public FiscalProfile(string firstname, string lastname, char sex, string dateOfBirth)
    {
        this.Firstname = firstname;
        this.Lastname = lastname;
        this.Sex = sex;
        this.DateOfBirth = dateOfBirth;
    }

    public string GetFiscalCode()
    {
        string code = "";
        
        // 3 consonants from lastname
        // if insufficient cons, add vowels AFTER cons
        // if insufficient vowels, add Xs
        code += GetStringWithoutVowels(Lastname.ToUpper());
        
        return code.ToUpper();
    }
    private string GetStringWithoutVowels(string input)
    {
        return input
            .Replace("A", string.Empty)
            .Replace("E", string.Empty)
            .Replace("I", string.Empty)
            .Replace("O", string.Empty)
            .Replace("U", string.Empty);
    }
}
