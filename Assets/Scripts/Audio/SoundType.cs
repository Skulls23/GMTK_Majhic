using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrannyCore{
    namespace Audio{

    /// <summary>
    /// Sert à définir les actions sonores de manière générique
    /// Préfixes:
    ///     * M_ : musique
    ///     * SFX_ : effet sonore
    ///
    /// Liste pour référence:
    /// - Son de la voiture de la mamie (continu et très léger)
    /// - Son des machines à sous qui tirent (faire 3-4 variantes pour la randomisation), cela pourrait être les sons des machines de casino (quand on actionne une machine)
    /// - Bonus (ça pourrait être un son type jackpot)
    /// - Malus (ça peut être un son de défaite des machines à sous)
    /// - Son de pièces (projectiles tirés par les machines, 3-4 variantes)
    /// - Son projectiles billets (projectiles tirés par la mamie, 3-4 variantes)
    /// - Son lancé de dés (lancé et retombée)
    /// - Bruit de dégâts/mort
    /// </summary>

        public enum SoundType {
            // Types d'audio à ajouter. Les nommer en fonction des actions. Exemples: "SFX_TirJoueur_01" ou "M_ThemePrincipal" - 22
            //------------------------------3
            None,
            M_Musique_01,
            M_Musique_02,
            //------------------------------1
            SFX_VoitureDeMamie,
            //------------------------------4
            SFX_MachineASous_01,
            SFX_MachineASous_02,
            SFX_MachineASous_03,
            SFX_MachineASous_04,
            //------------------------------2
            SFX_Bonus_01,
            SFX_Malus_01,
            //------------------------------4
            SFX_Piece_01,
            SFX_Piece_02,
            SFX_Piece_03,
            SFX_Piece_04,
            //------------------------------4
            SFX_Projectile_01,
            SFX_Projectile_02,
            SFX_Projectile_03,
            SFX_Projectile_04,
            //------------------------------2
            SFX_De_Lance_01,
            SFX_De_Retombe_02,
            //------------------------------2
            SFX_Degats_01,
            SFX_Mort_01
            
        }
    }
}
