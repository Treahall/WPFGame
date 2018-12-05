﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame.Entities
{
    class Mandrake : Enemy
    {
        public Mandrake() : base() { }

        public override void LoadAnimations()
        {

            damageindex = attackAnimation.Count / 2;
        }

        public override void setSpeed()
        {
            //speed = int.Parse(enemydata.GetString("SkeletonSpeed"));
        }

        public override int getAttackDistance()
        {
            return 30;
        }

        public override bool inAttackRange()
        {
            //if distance between user and enemy is less then attackDistance make inattackrange true / else false
            if (Math.Abs(Position.X - theUser.Position.X) <= getAttackDistance())
                return true;
            else
                return false;
        }

        public override void SetVelocity()
        {
            throw new NotImplementedException();
        }
    }
}
