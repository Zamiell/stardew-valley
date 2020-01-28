using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Tools;
using System;

namespace BobberDistance
{
    public class BobberDistanceMod : Mod
    {
        private int dist = -1;
        private double dist2 = -1;

        private Config config;

        private Color color;
    
        public override void Entry(IModHelper helper)
        {
            this.config = this.Helper.ReadConfig<Config>();
            try
            {

                this.color = this.Helper.Reflection.GetProperty<Color>(typeof(Color), this.config.Color, false).GetValue();
            }
            catch
            {
                this.Monitor.Log($"Invalid color {this.config.Color}, using black.", LogLevel.Error);
                this.color = Color.Black;
            }
            
            helper.Events.GameLoop.UpdateTicked += this.GameLoop_UpdateTicked;
            helper.Events.Display.RenderingHud += this.Display_RenderingHud;
            helper.Events.GameLoop.OneSecondUpdateTicked += this.GameLoop_OneSecondUpdateTicked;

        }

        private void GameLoop_OneSecondUpdateTicked(object sender, StardewModdingAPI.Events.OneSecondUpdateTickedEventArgs e)
        {
            if (this.dist != -1 && this.config.PrintToConsole)
            {
                this.Monitor.Log($"#Tiles from land: {this.dist}", LogLevel.Info);
                this.Monitor.Log($"Pixels from player: {this.dist2}", LogLevel.Info);
            }
        }

        private void Display_RenderingHud(object sender, StardewModdingAPI.Events.RenderingHudEventArgs e)
        {
            if (this.dist != -1 && this.config.ShowInGame)
            {
                Vector2 pos = Game1.player.getLocalPosition(Game1.viewport) + new Vector2(0, 64);
                Vector2 pos2 = pos + new Vector2(0, 64);

                e.SpriteBatch.DrawString(Game1.dialogueFont, $"#Tiles from land: {this.dist}", pos, this.color);
                e.SpriteBatch.DrawString(Game1.dialogueFont, $"Pixels from player: {this.dist2}", pos2, this.color);

                
            }
        }

        private void GameLoop_UpdateTicked(object sender, StardewModdingAPI.Events.UpdateTickedEventArgs e)
        {
            if (Game1.player.CurrentTool is FishingRod rod && (rod.isFishing || rod.isNibbling))
            {
                this.dist = this.Helper.Reflection.GetField<int>(rod, "clearWaterDistance").GetValue();
                this.dist2 = Math.Round(Math.Sqrt(Math.Pow(Game1.player.Position.X - rod.bobber.X, 2) + Math.Pow(Game1.player.Position.Y - rod.bobber.Y, 2)), 1);

            }
            else
            {
                this.dist = -1;
                this.dist2 = -1;
            }
        }
    }
}
