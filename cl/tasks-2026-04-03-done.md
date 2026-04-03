# Tasks

## 1. Add "Profession" column
- Add a `Profession` text field to the `Client` model
- Add a "Profession" column to the main DataGridView
- Add a "Profession" input field in the Add/Edit client dialog

## 2. Add "Organization" column with unique key
- Add an `OrganizationKey` field to the `Client` model (3-character string, e.g. `krl`)
- Add an "Organization" column to the main DataGridView — display the 3-character key
- In the Add/Edit client dialog, replace free-text input with a ComboBox that lets the user select an organization from the Organizations reference list
- The ComboBox should display the organization name, but store and show the 3-character key in the client table

## 3. Organizations reference (dictionary/справочник)
- Create an `Organization` model with two fields:
  - `Key` — unique 3-character string (e.g. `krl`)
  - `Name` — full organization name (e.g. `Karlin Ltd`)
- Store organizations in a separate JSON file: `organizations.json`
- The file should be pre-populated with a few example entries so the app works immediately; the user will replace them with their own list manually

## 4. Organizations management UI
- Add a toolbar button "Organizations" (or menu item) on the main form
- Clicking it opens a separate "Organizations" form/dialog with:
  - A DataGridView listing all organizations (Key | Name)
  - "Add" button — opens a small dialog with two fields: Key (max 3 chars, uppercase enforced) and Name
  - "Edit" button — opens the same dialog pre-filled with selected row
  - "Delete" button — removes selected organization (with confirmation; blocked if any client uses it)
  - Changes are saved to `organizations.json` immediately on every action

## 5. Validation rules
- Organization Key: exactly 3 characters, letters only, stored in lowercase, must be unique
- Profession: optional, max 100 characters
- Deleting an organization that is assigned to one or more clients must be blocked with a clear error message
