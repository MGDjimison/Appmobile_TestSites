using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Notes.Models;
using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net;

namespace Notes.Data
{
    public class NoteDatabase
    {
        readonly SQLiteAsyncConnection database;
        readonly HttpClient client = new HttpClient();

        public NoteDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Note>().Wait();
        }

        public Task<List<Note>> GetNotesAsync()
        {
            //Get all notes.
            return database.Table<Note>().ToListAsync();
        }

        public Task<Note> GetNoteAsync(int id)
        {
            // Get a specific note.
            return database.Table<Note>() 
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }
        
        public Task<List<Note>> GetNotesDeletedAsync(bool active)
        {
            // recupere les notes inactives
            return database.Table<Note>()
                .Where(note => note.isActive == active).ToListAsync();
        }
        
        public Task<int> SaveNoteAsync(Note note)
        {
            if (note.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(note);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(note);
            }
        }
        
        public Task<int> DesactiveNote(Note note)
        {
            
            if (note.ID != 0)
            {
                // Update an existing note.
                note.isActive = false;
            }
            return database.UpdateAsync(note);
        }

        public Task<int> ActiveNote(Note note)
        {

            if (note.ID != 0)
            {
                // Update an existing note.
                note.isActive = true;
            }
            return database.UpdateAsync(note);
        }   

        public Task<int> DeleteNoteAsync(Note note)
        {
            // Delete a note.
            return database.DeleteAsync(note);
        } 

        public async Task<bool> TestSiteAsync(string url)
        {
            try
            {
                bool result = false;
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception)
            {
                Console.WriteLine("\nException Caught!");
            }
            return false;
        }

        public bool Ping(string ip)
        {
            System.Diagnostics.Debug.WriteLine("DEMARRAGE DE LA FONCTION PING");
            try
            {
                bool res = false;
                // classe ping native qui permet d'envoyer un ping
                Ping ping = new Ping();
                // classe native qui renvoie le statut du ping
                PingReply reply = ping.Send(ip, 5000);

                if (reply.Status == IPStatus.Success)
                {
                    res = true;
                }

                return res;
            }
            catch (PingException ex)
            {
                System.Diagnostics.Debug.WriteLine("LOG Exception " + ex.Message);
            }
            return false;
        }

    }
}