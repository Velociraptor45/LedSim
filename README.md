# LedSim

LedSim wurde erstellt, damit du auch ohne entsprechende Hardware eine LED-Matrix am Computer simulieren kannst.

## Installation

Du kannst LedSim zu deinem C#-Projekt hinzufügen, indem du unten stehenden Code in deine .csproj-Projektdatei einfügst.

```
<ItemGroup>
  <PackageReference Include="LedSim" Version="*" />
</ItemGroup>
```

## Benutzung

Sobald das Paket hinzugefügt ist, kannst du anfangen, eine LED auf der Konsole zu simulieren. Instanziiere dazu ein neues `Led` Objekt.

```cs
using var led = new Led(5, 3, Color.Black);
```

In diesem Fall hat die LED-Matrix 5 Spalten & 3 Zeilen (also 15 Zellen/LEDs) und standardmäßig sind die LEDs schwarz. Um einer LED jetzt eine andere Farbe zu geben, kannst du sie über ihren Index ansprechen:

### Zugriff
```cs
led[0, 2] = new Color(255, 100, 0);
```

Da wir immer bei 0 anfangen zu zählen, wird die Farbe der LED in der ersten Spalte & dritten Reihe verändert.

### Zurücksetzen
Um alle LEDs auf die Ausgangsfarbe zurückzusetzen, kannst du die `Clear` methode benutzen.
```cs
led.Clear();
```

### Warten
Wenn du eine bestimmte Zeit warten willst bevor du den nächsten Befehl ausführst, kannst du mit `Wait` eine Zeit in Millisekunden angeben, die das Programm warten soll. Hier wartet das Programm eine Sekunde (= 1000 Millisekunden):

```cs
led.Wait(1000);
```

## Beispiel
Hier siehst du ein etwas komplexeres Beispiel, bei dem eine 5x3 Matrix durchlaufen wird und nacheinander die LEDs orange leuchten.

```cs
using LedSim;
using Spectre.Console;

const int rowCount = 3;
const int columnCount = 5;

using var led = new Led(columnCount, rowCount, Color.Black);

for (int row = 0; row < rowCount; row++)
{
    for (int col = 0; col < columnCount; col++)
    {
        led.Wait(500);
        led.Clear();
        led[col, row] = new Color(255, 100, 0);
    }
}

led.Wait(500);
led.Clear();
```