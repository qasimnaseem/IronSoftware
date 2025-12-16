# 📱 Keypad Input Decoder

A C# utility that decodes old mobile phone (multi-tap) keypad inputs into readable text — just like typing on a classic Nokia 3310!  
This project demonstrates input decoding, string processing, and clean extensible architecture using interfaces.

---

## 🚀 Features

- ✅ Decodes classic *multi-tap keypad* input (e.g., `4433555 555666# → HELLO`)
- ✅ Supports pause (` `), backspace (`*`), and end-of-input (`#`) symbols
- ✅ Validates input and throws clear exceptions for invalid formats
- ✅ Designed with extensibility in mind — ready for future decoders (e.g., predictive text)
- ✅ Clean, testable C# code using modern conventions

---

## 🧠 Example

### Input
4433555 555666#

### Output
HELLO

### How It Works
| Key 	| Characters 	|
|-----	|-------------	|
| 2 	| A, B, C 		|
| 3 	| D, E, F 		|
| 4 	| G, H, I 		|
| 5 	| J, K, L 		|
| 6 	| M, N, O 		|
| 7 	| P, Q, R, S 	|
| 8 	| T, U, V 		|
| 9 	| W, X, Y, Z 	|
| 0 	| (space) 		|

You press a key multiple times to select a letter, just like on old mobile phones.  
Example: `44` → **H**, `33` → **E**, etc.

---

## 🧩 Architecture Overview

### **Core Class**
`KeypadInputDecoder`
- Responsible for converting keypad input sequences to text.
- Handles multi-tap decoding logic, including wrap-around and input control characters.

### **Interfaces**
`IKeypadInputDecoder`
- Defines a contract for all keypad input decoders.
- Enables you to plug in different decoding strategies (e.g., T9 or Morse keypad decoding).

### **Constants**
`AppConstants`
- Organized into subcategories for clarity:
  - `InfoMessage`
  - `ErrorMessage`
  - `PromptMessage`

---

## 💻 Usage Example

```csharp
using System;
using MyApp.Decoders;

class Program
{
    static void Main()
    {
        IKeypadInputDecoder decoder = new KeypadInputDecoder();

        Console.WriteLine(AppConstants.PromptMessage.EnterKeypadInput);
        string? input = Console.ReadLine();

        if (string.Equals(input, "X", StringComparison.OrdinalIgnoreCase))
            return;

        try
        {
            string output = decoder.Decode(input!);
            Console.WriteLine($"Decoded Text: {output}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

⚙️ Input Rules

|   Symbol	|	Meaning                                 |
|---------- | ----------------------------------------- |
|   0–9		|	Keypad numbers for characters           |
|   `*`		|	Backspace (removes previous character   |
|   (space)	|	Pause between key sequence              |
|   `#`		|	End of input (required)                  |

🧪 Example Test Case 
|   Input			    |	Output	|	Notes               |
| --------------------- | --------- | --------------------- |
|   2#					|	A		|	Single key press    |
|   22#					|	B		|	Multi-tap           |
|   999337777#			|	YES		|	Combination of keys |
|   44 33 555 555 666#	|	HELLO	|	Uses pauses         |
|   4433555*555666#		|	HELLO	|	Uses backspace      |

🛠️ Tech Stack

Language: C#  
Framework: .NET 8  
Paradigm: Object-Oriented Programming  
Tools: Visual Studio / VS Code  

🧰 Project Structure  
📦 IronSoftware  
├── 📂 Constants  
│   └── AppConstants.cs  
├── 📂 Interfaces  
│   └── IKeypadInputDecoder.cs  
├── 📂 Services  
│   └── KeypadInputDecoder.cs  
├── 📂 Tests  
│   └── KeypadInputDecoderTests.cs  
└── README.md  


🧭 Future Enhancements

🔹 Add Predictive Text (T9) decoder implementation

🔹 Include unit tests for input validation and edge cases

🔹 Build a console or web UI for user-friendly interaction

🔹 Add support for custom keypad mappings

## 🤖 AI Prompt

This project was inspired and assisted by an AI prompt used with ChatGPT.  
You can view the prompt here: [AI Prompt Link](https://chatgpt.com/share/69418e20-1c74-8001-8a67-f6bb8212c146)

📜 License

This project is released under the MIT License  

💬 Author  
Muhammad Qasim  
dev-qasim@hotmail.com
