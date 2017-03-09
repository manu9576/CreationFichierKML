///
///Copyright (c) 2013, E. Lemaistre
///All rights reserved.
///
///Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
///
///    Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
///    Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
///
///THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;


namespace CreationKML
{
    public class FichierGPS : IEnumerable
    {

        // variables menbres
        private List<double> values = new List<double>();

        private int m_nbValeur;
        private float m_xdel;


        public int Length
        {
            get { return m_nbValeur; }
            set { m_nbValeur = value; }
        }
        private string m_Name;

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }


        /// <summary>
        /// Contructeur: construit une liste de valeurs d'apres
        /// le fichier dont le chemin est passé en argument.
        /// </summary>
        /// <param name="chemin"></param>
        public FichierGPS(string chemin)
        {
            int nbOct, i = 0;

            if (!File.Exists(chemin)) throw new Exception("Le fichier n'existe pas...");
            // Si le fichier n'existe pas on lance une exception

            // ouverture du fichier et creation du lecteur binaire
            using (FileStream fs = new FileStream(chemin, FileMode.Open, FileAccess.Read))
            {
                BinaryReader lect = new BinaryReader(fs);

                string sLectureValeur;
                char cLectureCaratere;
                char[] sRechercheEntete;


                #region   Traitement du bloc de données CD
                try
                {
                    sRechercheEntete = lect.ReadChars(2);
                    // on recherche la chaine CD
                    while (!(sRechercheEntete[0] == 'C' && sRechercheEntete[1] == 'D'))
                    {
                        sRechercheEntete[0] = sRechercheEntete[1];
                        sRechercheEntete[1] = lect.ReadChar();
                    }
                    // on passe 3 ','
                    i = 0;
                    while (i < 3 && fs.Position < fs.Length - 1) { if (lect.ReadChar() == ',') i++; }

                    // on lit la valeur jusqu'a la virgule
                    sLectureValeur = null;
                    while ((cLectureCaratere = lect.ReadChar()) != ',')
                        sLectureValeur += cLectureCaratere;
                    // on convertie la string en valeur
                    m_xdel = float.Parse(sLectureValeur, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                }
                catch (Exception ex)
                {
                    throw new Exception("Impossible de Traiter la chaine CD  de la frequence d'ech : " + ex.Message);
                }
                #endregion


                #region Traitement du bloc de données CN
                try
                {
                    // Recherche de la premiére clé CN
                    sRechercheEntete = lect.ReadChars(2);
                    while (!(sRechercheEntete[0] == 'C' && sRechercheEntete[1] == 'N'))
                    {
                        sRechercheEntete[0] = sRechercheEntete[1];
                        sRechercheEntete[1] = lect.ReadChar();
                    }
                    // on passe 7 ','
                    i = 0;
                    while (i < 7 && fs.Position < fs.Length - 1) { if (lect.ReadChar() == ',') i++; }

                    // on lit la valeur
                    sLectureValeur = null;

                    while ((cLectureCaratere = lect.ReadChar()) != ',')
                        sLectureValeur += cLectureCaratere;

                    m_Name = sLectureValeur;

                 
                }
                catch (Exception ex)
                {
                    throw new Exception("Impossible traiter le bloc Cb contenant le nombre d'octet / valeurs: " + ex.Message);
                }
                #endregion


                #region Traitement du bloc de données Cb
                try
                {
                    // Recherche de la premiére clé Cb
                    sRechercheEntete = lect.ReadChars(2);
                    while (!(sRechercheEntete[0] == 'C' && sRechercheEntete[1] == 'b'))
                    {
                        sRechercheEntete[0] = sRechercheEntete[1];
                        sRechercheEntete[1] = lect.ReadChar();

                    }
                    // on passe 8 ','
                    i = 0;
                    while (i < 8 && fs.Position < fs.Length - 1) { if (lect.ReadChar() == ',') i++; }

                    // on lit la valeur
                    sLectureValeur = null;

                    while ((cLectureCaratere = lect.ReadChar()) != ',')
                        sLectureValeur += cLectureCaratere;

                    nbOct = int.Parse(sLectureValeur);
                    m_nbValeur = nbOct / 4;
                }
                catch (Exception ex)
                {
                    throw new Exception("Impossible traiter le bloc Cb contenant le nombre d'octet / valeurs: " + ex.Message);
                }
                #endregion

                # region Traitement du bloc de données CS
                try
                {
                    // Recherche de la premiére clé CS
                    sRechercheEntete = lect.ReadChars(2);

                    while (!(sRechercheEntete[0] == 'C' && sRechercheEntete[1] == 'S'))
                    {
                        sRechercheEntete[0] = sRechercheEntete[1];
                        sRechercheEntete[1] = lect.ReadChar();
                    }

                    // ***** Le nombre de valeurs Binaire se situe 2 virgule apres la clé
                    i = 0;
                    while (i < 4 && fs.Position < fs.Length - 1) { if (lect.ReadChar() == ',') i++; }
                }
                catch (Exception ex)
                {
                    throw new Exception("Impossible de Traiter la chaine CS : " + ex.Message);
                }
                #endregion

                #region  Conversion des données binaires
                try
                {
                    // tableau recevant les valeurs binaires
                    Byte[] val = new Byte[nbOct];
                    val = lect.ReadBytes(nbOct);

                    for (int j = 0; j < m_nbValeur; j++)
                        values.Add(BitConverter.ToSingle(val, j * 4));
                }
                catch (Exception ex)
                {
                    throw new Exception("Impossible delire les valeurs binaire : " + ex.Message);
                    throw;
                }
                #endregion

            }

        }

        /// <summary>
        /// Renvoie le nmobre de valeurs contenues dans le fichier
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return m_nbValeur;
        }

        /// <summary>
        /// Permet l'utilisation des boucles forEach sur la liste de valeur
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)values.GetEnumerator();
        }

        /// <summary>
        /// Renvoie la valeur à l'indice demandé
        /// Si l'indice n'est pas cohérants, on renvoie la derniere valeur de la list
        /// </summary>
        /// <param name="i"></param>
        /// <returns>Valeur à l'indice demandé</returns>
        public double get_Value(int i)
        {
            if (i < values.Count && i >= 0)
                return values[i];
            else
            {
                return values[values.Count - 1];
            }
        }

        /// <summary>
        /// Renvoie la période d'échantillonnage de la voie
        /// </summary>
        /// <returns>1/fe</returns>
        public float xDelta()
        {
            return m_xdel;
        }

    }
}
