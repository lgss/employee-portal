using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleVisionAPI
{
    public class VisionAPIResponse
    {
        public Response[] responses { get; set; }
    }

    public class Response
    {
        public Safesearchannotation safeSearchAnnotation { get; set; }
        public Webdetection webDetection { get; set; }
        public Labelannotation labelAnnotation { get; set; }
        public Faceannotation[] faceAnnotations { get; set; }

    }

    public class Safesearchannotation
    {
        public string adult { get; set; }
        public string spoof { get; set; }
        public string medical { get; set; }
        public string violence { get; set; }
        public string racy { get; set; }
    }

    public class Webdetection
    {
        public Webentity[] webEntities { get; set; }
        public Bestguesslabel[] bestGuessLabels { get; set; }
    }

    public class Webentity
    {
        public string entityId { get; set; }
        public float score { get; set; }
        public string description { get; set; }
    }

    public class Bestguesslabel
    {
        public string label { get; set; }
        public string languageCode { get; set; }
    }

    public class Labelannotation
    {
        public string mid { get; set; }
        public string description { get; set; }
        public float score { get; set; }
    }

    public class Faceannotation
    {
        public Boundingpoly boundingPoly { get; set; }
        public string blurredLikelihood { get; set; }
        public string headwearLikelihood { get; set; }
    }

    public class Boundingpoly
    {
        public Vertex[] vertices { get; set; }
    }

    public class Vertex
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}