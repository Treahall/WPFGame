﻿using System;
using System.Resources;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFGame.Entities;
using WPFGame.Animation;
using WPFGame.stageGraphics;
using static System.Windows.Media.Imaging.WriteableBitmapExtensions;
using System.Collections;

namespace WPFGame.Entities
{
    public class Player : GameEntity
    {
        bool jumping;
        int jumpForce = -120, force;

        public Player() : base()
        {
            animation = animationLists.CharacterIdol;
            AnimationIndex = 0;
            jumping = false;
            //Initial position
            Position = new System.Numerics.Vector2((float)(graphics.WindowWidth/2), floor -= GetSpriteHeight());
        }

        public override void setSpeed()
        {
            speed = int.Parse(defaultdata.GetString("Speed"));
        }

        public override void Draw(WriteableBitmap surface)
        {
            base.Draw(surface);
        }

        public void RunJumpAlg()
        {
            //wall bounds for when jumping
            if (!(Position.X >= leftbound && Position.X <= rightbound))
            {
                //if further right
                if (Position.X >= graphics.WindowWidth/2)
                     Position = new System.Numerics.Vector2((float)rightbound, Position.Y);
                //if further left
                else
                    Position = new System.Numerics.Vector2(0, Position.Y);

                Velocity = new System.Numerics.Vector2(0, Velocity.Y);
            }

            //Stop falling at the bottom
            if (Position.Y > floor)
            {
                Velocity = new System.Numerics.Vector2(Velocity.X, 0); // sets vertical velocity to zero
                Position = new System.Numerics.Vector2(Position.X, floor); // resets the base vertical position
                jumping = false; //stops jumping 
                force = jumpForce;
                Fpa = 10;
                animation = animationLists.CharacterIdol;
            }
            else
            {
                force += 15;
                Velocity += new System.Numerics.Vector2(0, force);
            } 
        }



        public override void CalculateDirection()
        {
            PreviousDirection = Currentdirection;

            //Don't move (Left and right cancel out)
            if (Keyboard.IsKeyDown(Key.Left) && Keyboard.IsKeyDown(Key.Right) && !jumping)
                Currentdirection = Direction.idle;
            //Move left
            else if (Keyboard.IsKeyDown(Key.Left) && Position.X > leftbound)
                Currentdirection = Direction.left;
            // Move right
            else if (Keyboard.IsKeyDown(Key.Right) && Position.X < rightbound)
                Currentdirection = Direction.right;
            // Idle
            else if(!jumping)
                Currentdirection = Direction.idle;

            //sperated for independent jumping action
            if (Keyboard.IsKeyDown(Key.Space) && !jumping)
            {
                jumping = true;
                AnimationIndex = 0;
                animation = animationLists.CharacterJump;
                Fpa = 5;
            }
        }

        public override void SetVelocity()
        {
            CalculateDirection();

            //wall bounds for when jumping.
            if (!(Position.X >= leftbound && Position.X <= rightbound))
                Velocity = new System.Numerics.Vector2(0, Velocity.Y);

            //resets index when animation is changed.
            if (PreviousDirection != Currentdirection) AnimationIndex = 0;

            switch (Currentdirection)
            {
                case Direction.idle:
                    Velocity = new System.Numerics.Vector2(0, Velocity.Y);
                    if (!jumping) animation = animationLists.CharacterIdol;
                    break;
                case Direction.left:
                    if (Position.X >= leftbound) Velocity = new System.Numerics.Vector2(-speed, Velocity.Y);
                    if (!jumping) animation = animationLists.CharacterRun;
                    FlipEntity = true;
                    break;
                case Direction.right:
                    if (Position.X <= rightbound) Velocity = new System.Numerics.Vector2(speed, Velocity.Y);
                    if (!jumping) animation = animationLists.CharacterRun;
                    FlipEntity = false;
                    break;
            }
            if (jumping == true) RunJumpAlg();
            pushPositionX();
        }

        //Store the Players X position as a resource so enemies can base movement on it.
        void pushPositionX()
        {
            Hashtable newResource = new Hashtable();
            ResourceReader reader = new ResourceReader(savedpath);
            ResourceWriter writer = new ResourceWriter(savedpath);
            if(reader!= null)
            {
                foreach (DictionaryEntry entry in reader)
                {
                    if ((string)entry.Key == "PositionX")
                    {
                        newResource.Add(entry.Key.ToString(), Position.X.ToString());
                        writer.AddResource(entry.Key.ToString(), Position.X.ToString());
                    }
                    else if (entry.Value == null)
                    {
                        newResource.Add(entry.Key, "");
                        writer.AddResource(entry.Key.ToString(), "");
                    }
                    else
                    {
                        newResource.Add(entry.Key, entry.Value);
                        writer.AddResource(entry.Key.ToString(), entry.Value.ToString());
                    }
                }
                reader.Close();
            }
            writer.Generate();
            writer.Close();
        }

        public override void GameTick(float millisecondsPassed)
        {
            base.GameTick(millisecondsPassed);
        }
    }
}