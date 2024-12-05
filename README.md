# 🔥 **SCP-372 Plugin for SCP: Secret Laboratory** 🔥

![Exiled](https://img.shields.io/badge/Exiled-8.14.0-blue.svg) ![Language](https://img.shields.io/badge/Language-C%23-brightgreen.svg) ![License](https://img.shields.io/badge/License-MIT-yellow.svg)

## 🌍 **Description ENG**

**SCP-372 Plugin** is an advanced server plugin for SCP: Secret Laboratory that introduces **SCP-372** – an invisible and exceptionally fast entity. SCP-372 becomes temporarily visible after shooting or interacting with objects.

### ✨ **Features**
- 📌 **Invisibility**: Automatically applies the `Invisible` effect to SCP-372.
- ⏱️ **Temporary Visibility**: SCP-372 becomes visible for a configurable amount of time after performing actions (e.g., opening doors, shooting).
- ⚙️ **Full Configuration**: Customize the starting class, health, visibility duration, and more.
- 🪄 **Dynamic Correction**: Automatically corrects invisibility effect if removed unexpectedly.
- 🛠️ **Debugging**: Additional console logs (when enabled in the config).

### 🚀 **Requirements**
- **Exiled API** version `8.14.0`.
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

**SCP-372 Plugin** to zaawansowany plugin na serwery SCP: Secret Laboratory, który dodaje **SCP-372** – niewidzialną i wyjątkowo szybką jednostkę. Działa TYLKO na exiledzie w wersji 8.14.0.

### ✨ **Funkcje**
- 📌 **Niewidzialność**: Automatyczne ustawienie efektu `Invisible` dla SCP-372.
- ⏱️ **Tymczasowa widzialność**: SCP-372 staje się widzialny na określony czas po akcji (np. otwieranie drzwi, strzelanie).
- ⚙️ **Pełna konfiguracja**: Możliwość ustawienia klasy startowej, zdrowia, czasu widzialności i więcej.
- 🪄 **Dynamiczna korekcja**: System automatycznie koryguje efekt niewidzialności, jeśli zostanie usunięty.
- 🛠️ **Debugowanie**: Dodatkowe logi w konsoli (jeśli włączone w konfiguracji).

### 🚀 **Wymagania**
- **Exiled API** w wersji `8.14.0`.

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
