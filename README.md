# 🔥 **SCP-372 Plugin for SCP: Secret Laboratory** 🔥

![Downloads](https://img.shields.io/github/downloads/MyMQL/SCP-372/total?label=Downloads&style=flat) ![Exiled](https://img.shields.io/badge/Exiled-9.5.1-blue.svg) ![Language](https://img.shields.io/badge/Language-C%23-brightgreen.svg) ![License](https://img.shields.io/badge/License-MIT-yellow.svg)

## 🌍 **Description ENG**

**SCP-372** is an advanced server plugin for SCP: Secret Laboratory that introduces **SCP-372** – an invisible and exceptionally fast entity. SCP-372 becomes temporarily visible after shooting, interacting with objects, entering certain areas like the surface, using items, or speaking in voice chat.

### ✨ **Features**
- 📌 **Invisibility**: Automatically applies the `Invisible` effect to SCP-372.
- ⏱️ **Temporary Visibility**: SCP-372 becomes visible for a configurable amount of time after performing actions (e.g., opening doors, shooting, using items, or speaking in voice chat).
- 🌐 **Surface Detection**: SCP-372 becomes permanently visible when on the surface and invisible again when returning underground.
- ⚙️ **Full Configuration**: Customize the starting class, health, visibility duration, item usage visibility, voice chat visibility, spawn chance, and more.
- 🛡️ **Player Count Check**: SCP-372 will only spawn if a minimum configurable number of players is on the server.
- 📌 **Room-Specific Spawn**: Configure SCP-372 to spawn in a specific room (default: `HCZ049`). Note: For available room names, see the [RoomType documentation](https://docs.exiled-team.net/api/Exiled.API.Enums.RoomType.html). Best left at the default value, as this feature isn't fully stable yet.
- 🪄 **Dynamic Correction**: Automatically corrects invisibility effect if removed unexpectedly.
- 🔔 **Escape Detection**: Broadcast a configurable CASSIE announcement when SCP-372 escapes.
- 📢 **Spawn Announcement**: Broadcast a configurable CASSIE announcement when SCP-372 spawns.
- 🔔 **Death Announcement**: Broadcast a configurable CASSIE announcement when SCP-372 dies.
- 🔧 **API Support**: Expose events to integrate SCP-372's behavior with other plugins or systems.
- 🛠️ **Debugging**: Additional console logs (when enabled in the config).

### 🚀 **Requirements**
- **Exiled API** version `9.0.1` or higher.
- Ability to drag and drop a file 🤩

### 🔧 **Configuration**
The `config.yml` file allows full control over the plugin:

```yaml
scp372plugin:
  is_enabled: true # Enable or disable the plugin
  debug: true # Enable debug logs
  visibility_duration: 2.0 # Duration of visibility after an action (in seconds)
  starting_role: ClassD # The starting class for SCP-372
  starting_health: 100 # Starting health of SCP-372
  broadcast_message: "<b><color=red>You are SCP-372!</color></b>" # Broadcast message shown on assignment
  spawn_chance: 30 # Percent chance for SCP-372 to spawn
  enable_cassie_on_escape: true # Enable or disable CASSIE escape announcements
  cassie_message_on_escape: "SCP 3 7 2 has successfully escaped" # CASSIE announcement for SCP-372 escape
  enable_cassie_on_spawn: true # Enable or disable CASSIE spawn announcements
  cassie_message_on_spawn: "SCP 3 7 2 has breached containment" # CASSIE announcement for SCP-372 spawn
  enable_cassie_on_death: true # Enable or disable CASSIE death announcements
  cassie_message_on_death: "SCP 3 7 2 has been neutralized" # CASSIE announcement for SCP-372 death
  visible_when_using_items: true # Should SCP-372 become visible when using items (e.g., medkits, adrenaline)?
  visible_when_speaking: true # Should SCP-372 become visible while speaking in voice chat (Q key)?
  speaking_visibility_duration: 4.0 # Duration (in seconds) SCP-372 stays visible after speaking in voice chat
  min_players: 8 # Minimum number of players required for SCP-372 to spawn
  spawn_room_type: HCZ049 # RoomType ID for SCP-372 spawn location (e.g., "HCZ049"). For options, see https://docs.exiled-team.net/api/Exiled.API.Enums.RoomType.html
```

### 🛠️ **Installation**
1. Download the `.dll` file from the **Releases** section.
2. Place the file into the `Plugins` folder on your server.
3. Configure the plugin in the `config.yml` file you can use CTRL+F to easily find our part of that config.
4. Start your server and enjoy the ~~bugs~~ gameplay!

---

## 🔧 **Licensing**
This project is licensed under the **MIT License**, meaning you are free to use, modify, and distribute it, don't really care.

---

## 🧩 **Reporting Issues**
Found a bug or have a suggestion? Open an issue in this repository and provide detailed information about the problem or your idea.

---

## 🛠️ **Opis (PL)**

**SCP-372** to zaawansowany plugin na serwery SCP: Secret Laboratory, który dodaje **SCP-372** – niewidzialną i wyjątkowo szybką jednostkę. Działa TYLKO na exiledzie w wersji 9.0.1.

### ✨ **Funkcje**
- 📌 **Niewidzialność**: Automatyczne ustawienie efektu `Invisible` dla SCP-372.
- ⏱️ **Tymczasowa widzialność**: SCP-372 staje się widzialny na określony czas po akcji (np. otwieranie drzwi, strzelanie, używanie przedmiotów, mówienie przez czat głosowy).
- 🌐 **Wykrywanie powierzchni**: SCP-372 staje się widzialny na powierzchni i wraca do niewidzialności, gdy wraca pod ziemię.
- ⚙️ **Pełna konfiguracja**: Możliwość ustawienia klasy startowej, zdrowia, czasu widzialności, widoczności podczas używania przedmiotów i mówienia.
- 🛡️ **Minimalna liczba graczy**: SCP-372 pojawia się tylko, jeśli liczba graczy na serwerze spełnia minimalne wymaganie.
- 📌 **Respawn w określonym pokoju**: Konfiguracja pokoju, w którym SCP-372 się pojawi (domyślnie: `HCZ049`). Dla dostępnych nazw pokoi, zobacz [RoomType documentation](https://docs.exiled-team.net/api/Exiled.API.Enums.RoomType.html). Funkcja może być niestabilna, najlepiej zostawić domyślną wartość.
- 🪄 **Dynamiczna korekcja**: System automatycznie koryguje efekt niewidzialności, jeśli zostanie usunięty.
- 🔔 **Wykrywanie ucieczki**: Nadawanie ogłoszenia przez CASSIE o ucieczce SCP-372.
- 📢 **Ogłoszenie pojawienia się**: Nadawanie ogłoszenia przez CASSIE o pojawieniu się SCP-372.
- 🔔 **Ogłoszenie śmierci**: Nadawanie ogłoszenia przez CASSIE o śmierci SCP-372.
- 🔧 **Obsługa API**: Eksponowanie zdarzeń do integracji zachowania SCP-372 z innymi pluginami.
- 🛠️ **Debugowanie**: Dodatkowe logi w konsoli (jeśli włączone w konfiguracji).

### 🚀 **Wymagania**
- **Exiled API** w wersji `9.0.1` lub wyższej.

### 🔧 **Konfiguracja**
Plik `config.yml` pozwala na pełną kontrolę nad pluginem:

```yaml
scp372plugin:
  is_enabled: true # Włącz lub wyłącz plugin
  debug: true # Włącz logi debugowania
  visibility_duration: 2.0 # Czas widzialności po akcji (w sekundach)
  starting_role: ClassD # Klasa, jako która SCP-372 pojawia się na starcie
  starting_health: 100 # Początkowe zdrowie SCP-372
  broadcast_message: "<b><color=red>You are SCP-372!</color></b>" # Wiadomość na górze ekranu
  spawn_chance: 30 # Szansa na przypisanie SCP-372 (w procentach)
  enable_cassie_on_escape: true # Włącz lub wyłącz ogłoszenia CASSIE o ucieczce
  cassie_message_on_escape: "SCP 3 7 2 has successfully escaped" # Ogłoszenie CASSIE o ucieczce SCP-372
  enable_cassie_on_spawn: true # Włącz lub wyłącz ogłoszenia CASSIE o pojawieniu się
  cassie_message_on_spawn: "SCP 3 7 2 has breached containment" # Ogłoszenie CASSIE o pojawieniu się SCP-372
  enable_cassie_on_death: true # Włącz lub wyłącz ogłoszenia CASSIE o śmierci SCP-372
  cassie_message_on_death: "SCP 3 7 2 has been neutralized" # Ogłoszenie CASSIE o śmierci SCP-372
  visible_when_using_items: true # Czy SCP-372 staje się widzialny podczas używania przedmiotów (np. apteczek, adrenaliny)?
  visible_when_speaking: true # Czy SCP-372 staje się widzialny podczas mówienia w czacie głosowym (klawisz Q)?
  speaking_visibility_duration: 4.0 # Czas widzialności (w sekundach) SCP-372 po mówieniu w czacie głosowym
  min_players: 8 # Minimalna liczba graczy wymagana do przypisania SCP-372
  spawn_room_type: HCZ049 # Typ pokoju, w którym SCP-372 ma się zrespić (np. "HCZ049"). Lista dostępnych pokoi: https://docs.exiled-team.net/api/Exiled.API.Enums.RoomType.html
```

### 🛠️ **Instalacja**
1. Pobierz plik `.dll` z sekcji **Releases**.
2. Umieść plik w folderze `Plugins` na serwerze.
3. Skonfiguruj plugin w pliku `config.yml`.
4. Uruchom serwer.

---

## 🔧 **Licencja**
Projekt jest dostępny na licencji **MIT**, co oznacza, że możesz go swobodnie używać, modyfikować i udostępniać.

---

## 🧩 **Zgłaszanie błędów**
Znalazłeś błąd lub masz sugestię? Otwórz zgłoszenie w tym repozytorium i podaj szczegółowe informacje o problemie lub swoim pomyśle. Ale BŁAGAM, bez postów typu "nie działa napraw".
