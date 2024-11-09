// See https://aka.ms/new-console-template for more information

using System.Runtime.Intrinsics.X86;
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
profiles.Add(new FiscalProfile("Samantha", "Davidson", 'F', "20/2/1602"));
profiles.Add(new FiscalProfile("Bob", "Davidson", 'M', "1/1/1111"));
profiles.Add(new FiscalProfile("Al", "Yankovic", 'M', "23/10/1959"));

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
        return $"{GetSurnameSegement()} {GetFirstnameSegment()} {GetThirdSection()}";
    }

    private string GetThirdSection()
    {
        return "";
    }

    private string GetFirstnameSegment()
    {
        string code = "";

        // 3 consonants from first name
        code += GetStringWithoutVowels(Firstname.ToUpper());
        
        // if more than 3: 1st, 3rd, 4th
        if (code.Length > 3)
        {
            code = code.Remove(1, 1);
        }
        
        // if less than 3: replace with vowels
        if (code.Length < 3)
        {
            code += GetStringWithoutConsonants(Firstname.ToUpper());
        }
        
        // if still insufficient replace with Xs
        code = FillWithXs(code);
        
        // select first 3
        return code.Substring(0, 3);
    }

    private string GetSurnameSegement()
    {
        string code = "";
        // 3 consonants from lastname
        code += GetStringWithoutVowels(Lastname.ToUpper());
        
        // if insufficient cons, add vowels AFTER cons
        if (code.Length < 3)
        {
            code += GetStringWithoutConsonants(Lastname.ToUpper());
        }
        
        // if insufficient vowels, add Xs
        code = FillWithXs(code);
        
        // select first 3
        return code.Substring(0, 3);
    }

    private string GetStringWithoutConsonants(string input)
    {
        string output = "";
        foreach (char c in input)
        {
            if (c is 'A' or 'E' or 'I' or 'O' or 'U')
            {
                output += c;
            }
        }
        return output;
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

    private string FillWithXs(string input)
    {
        while (input.Length < 3)
        {
            input += 'X';
        }
        return input;
    }
}
