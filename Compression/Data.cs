using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compression
{
    /// <summary>
    /// Holds all the data for the program.
    /// </summary>
    /// <remarks>
    /// This is the data class. It is an information expert and should just
    /// hold all the necessary data for the class. This should be passed
    /// as a reference to classes that need to insert data into the file loader
    /// class's object, otherwise it can be used as a reference to read.
    /// </remarks>
    /// 
    public class Data
    {
        // are these still necessary?

        /// <summary>
        /// Original bitmap
        /// </summary>
        private Bitmap original;

        /// <summary>
        /// RGB to YCrCb bitmap
        /// </summary>
        private Bitmap RGBtoYCrCb;

        /// <summary>
        /// YCrCb to RGB bitmap
        /// </summary>
        private Bitmap YCrCbtoRGB;

        /// <summary>
        /// Public data for Y, Cb, Cr as both doubles and bytes
        /// </summary>
        public byte[,] yData;
        public byte[,] CbData;
        public byte[,] CrData;

        public byte[,] yData2;
        public byte[,] CbData2;
        public byte[,] CrData2;

        public double[,] yDiff;
        public double[,] cbDiff;
        public double[,] crDiff;

        public double[,] yDiffBlock;
        public double[,] cbDiffBlock;
        public double[,] crDiffBlock;

        public double[,] dyData;
        public double[,] dCbData;
        public double[,] dCrData;

        /// <summary>
        /// Private data for R, G and B data as bytes
        /// </summary>
        byte[,] rData;
        byte[,] gData;
        byte[,] bData;

        /// <summary>
        /// Private data for YCbCr data
        /// </summary>
        public Color[,] YCbCrData;
        public Color[,] YCbCrData2;

        /// <summary>
        /// Private Forward DCT data as doubles
        /// </summary>
        double[,] forwardDCTData;

        /// <summary>
        /// Private Inverse DCT data as bytes
        /// </summary>
        byte[,] inverseDCTData;

        /// <summary>
        /// Public int for the Original Width, Height, padded width, padded
        /// height.
        /// </summary>
        public int orgWidth, 
                   orgHeight, 
                   paddedWidth, 
                   paddedHeight;

        /// <summary>
        /// Public arrays for final data, YEncoding data, CbEncoding data,
        /// and CrEncoding data. as sbytes
        /// </summary>
        public sbyte[] finalData, finalDiffData;
        public sbyte[] yEncoded,
                       cbEncoded,
                       crEncoded,
                       yDiffEncoded,
                       cbDiffEncoded,
                       crDiffEncoded;

        public MotionVector[] finalMVData,
                              yMVEncoded,
                              cbMVEncoded,
                              crMVEncoded;

        /// <summary>
        /// Public header for the file, both for reading and saving.
        /// </summary>
        public Header gHead = new Header();

        /// <summary>
        /// Header for the motion vector image 1
        /// </summary>
        public Header mv1Head = new Header();

        /// <summary>
        /// Header for the motion vector image 2
        /// </summary>
        public Header mv2Head = new Header();

        /// <summary>
        /// Public header for the motion vector compressed file.
        /// </summary>
        public MHeader gMHead = new MHeader();

        /// <summary>
        /// Gets Y Bitmap
        /// </summary>
        /// <remarks>
        /// Gets the Y bitmap for the image based on the header
        /// </remarks>
        /// <param name="header">Header for the image size</param>
        /// <returns></returns>
        public Bitmap getYBitmap(Header header)
        {
            Bitmap outBmp = new Bitmap(header.getWidth(), header.getHeight());
            for (int y = 0; y < header.getHeight(); y++)
            {
                for (int x = 0; x < header.getWidth(); x++)
                {
                    //outBmp.SetPixel(x, y, Color.FromArgb(yData[x,y]/3, yData[x, y] / 3, yData[x, y] / 3));
                    outBmp.SetPixel(x, y, Color.FromArgb(yData[x, y], yData[x, y], yData[x, y]));
                }
            }

            return outBmp;
        }

        /// <summary>
        /// Gets Cr Bitmap
        /// </summary>
        /// <remarks>
        /// Gets the Cr bitmap for the image based on the header
        /// </remarks>
        /// <param name="header">Header for the image size</param>
        /// <returns></returns>
        public Bitmap getCrBitmap(Header header)
        {
            Bitmap outBmp = new Bitmap(header.getWidth(), header.getHeight());
            for (int y = 0; y < header.getHeight(); y++)
            {
                for (int x = 0; x < header.getWidth(); x++)
                {
                    //outBmp.SetPixel(x, y, Color.FromArgb(CrData[x, y] / 3, CrData[x, y] / 3, CrData[x, y] / 3));
                    outBmp.SetPixel(x, y, Color.FromArgb(CrData[x, y], CrData[x, y], CrData[x, y]));
                }
            }

            return outBmp;
        }

        /// <summary>
        /// Gets Cb Bitmap
        /// </summary>
        /// <remarks>
        /// Gets the Cb bitmap for the image based on the header
        /// </remarks>
        /// <param name="header">Header for the image</param>
        /// <returns></returns>
        public Bitmap getCbBitmap(Header header)
        {
            Bitmap outBmp = new Bitmap(header.getWidth(), header.getHeight());
            for (int y = 0; y < header.getHeight(); y++)
            {
                for (int x = 0; x < header.getWidth(); x++)
                {
                    //outBmp.SetPixel(x, y, Color.FromArgb(CbData[x, y] / 3, CbData[x, y] / 3, CbData[x, y] / 3));
                    outBmp.SetPixel(x, y, Color.FromArgb(CbData[x, y], CbData[x, y], CbData[x, y]));
                }
            }

            return outBmp;
        }

        /// <summary>
        /// Gets YCbCr Bitmap
        /// </summary>
        /// <remarks>
        /// Gets the YCbCr bitmap for the image based on the header
        /// </remarks>
        /// <param name="header">Header for the image</param>
        /// <returns></returns>
        public Bitmap getYCbCrBitmap(Header header)
        {
            Bitmap outBmp = new Bitmap(header.getWidth(), header.getHeight());
            for (int y = 0; y < header.getHeight(); y++)
            {
                for (int x = 0; x < header.getWidth(); x++)
                {
                    outBmp.SetPixel(x, y, YCbCrData[x, y]);
                }
            }

            return outBmp;
        }

        /// <summary>
        /// Generates the Bitmap for the image data
        /// </summary>
        /// <remarks>
        /// Generates the bitmap for the image
        /// </remarks>
        /// <param name="head">Header of the bitmap.</param>
        /// <returns>Bitmap</returns>
        public Bitmap generateBitmap(Header head) {
            Bitmap outBmp = new Bitmap(head.getWidth(), head.getHeight()); 
            for (int y = 0; y < head.getHeight(); y++) 
            { 
                for (int x = 0; x < head.getWidth(); x++) 
                { 
                    outBmp.SetPixel(x, y, Color.FromArgb(rData[x,y], gData[x,y], bData[x,y])); 
                }
            }
            return outBmp;
        }

        /// <summary>
        /// Generates the Bitmap for the image data
        /// </summary>
        /// <remarks>
        /// Generates the bitmap for the image
        /// </remarks>
        /// <param name="head">MHeader of the bitmap.</param>
        /// <returns>Bitmap</returns>
        public Bitmap generateBitmap(MHeader head)
        {
            Bitmap outBmp = new Bitmap(head.getWidth(), head.getHeight());
            for (int y = 0; y < head.getHeight(); y++)
            {
                for (int x = 0; x < head.getWidth(); x++)
                {
                    outBmp.SetPixel(x, y, Color.FromArgb(rData[x, y], gData[x, y], bData[x, y]));
                }
            }
            return outBmp;
        }

        /// <summary>
        /// Generates the Bitmap for the image data
        /// </summary>
        /// <remarks>
        /// Generates the bitmap for the image
        /// </remarks>
        /// <returns></returns>
        public Bitmap generateBitmap(Color[,] data)
        {
            Bitmap outBmp = new Bitmap(gHead.getWidth(), gHead.getHeight());
            for (int y = 0; y < gHead.getHeight(); y++)
            {
                for (int x = 0; x < gHead.getWidth(); x++)
                {
                    outBmp.SetPixel(x, y, data[x,y]);
                }
            }
            return outBmp;
        }

        /// <summary>
        /// Initializes the YCbCr double byte array.
        /// </summary>
        public Color[,] createYCbCrArray(Header head, byte[,] luma, byte[,] cb, byte[,] cr)
        {
            Color[,] output = new Color[head.getWidth(), head.getHeight()];
            for(int x = 0; x < head.getWidth(); x++)
            {
                for(int y = 0; y < head.getHeight(); y++)
                {
                    output[x, y] = Color.FromArgb(luma[x, y], cb[x, y], cr[x, y]);
                }
            }
            return output;
        }

        /// <summary>
        /// Getters for all the data variables
        /// </summary>
        public Bitmap getOriginal() { return this.original; }
        public Bitmap getRGBtoYCrCb() { return this.RGBtoYCrCb; }
        public Bitmap getYCrCbtoRGB() { return this.YCrCbtoRGB; }
        public double[,] getForwardDCTData() { return this.forwardDCTData; }

        //public double[,] getInverseDCTData() { return this.inverseDCTData; }
        //testing
        public byte[,] getInverseDCTData() { return this.inverseDCTData; }
        public byte[,] getyData() { return this.yData; }
        public byte[,] getCbData() { return this.CbData; }
        public byte[,] getCrData() { return this.CrData; }
        public byte[,] getyData2() { return this.yData2; }
        public byte[,] getCbData2() { return this.CbData2; }
        public byte[,] getCrData2() { return this.CrData2; }
        public double[,] getdyData() { return this.dyData; }
        public double[,] getdCbData() { return this.dCbData; }
        public double[,] getdCrData() { return this.dCrData; }
        public byte[,] getrData() { return this.rData; }
        public byte[,] getgData() { return this.gData; }
        public byte[,] getbData() { return this.bData; }
        public Color[,] getYCrCbData() { return this.YCbCrData; }
        public Color[,] getYCrCbData2() { return this.YCbCrData2; }

        /// <summary>
        /// Setters for all the data variables
        /// </summary>
        public void setOriginal(Bitmap bmp) { this.original = bmp; }
        public void setRGBtoYCrCb(Bitmap bmp) { this.RGBtoYCrCb = bmp; }
        public void setYCrCbtoRGB(Bitmap bmp) { this.YCrCbtoRGB = bmp; }
        public void setForwardDCTData(double[,] data) { this.forwardDCTData = data; }

        //public void setInverseDCTData(double[,] data) { this.inverseDCTData = data; }
        //testing
        public void setInverseDCTData(byte[,] data) { this.inverseDCTData = data; }
        public void setyData(byte[,] data) { this.yData = data; }
        public void setCbData(byte[,] data) { this.CbData = data; }
        public void setCrData(byte[,] data) { this.CrData = data; }
        public void setyData2(byte[,] data) { this.yData2 = data; }
        public void setCbData2(byte[,] data) { this.CbData2 = data; }
        public void setCrData2(byte[,] data) { this.CrData2 = data; }
        public void setdyData(double[,] data) { this.dyData = data; }
        public void setdCbData(double[,] data) { this.dCbData = data; }
        public void setdCrData(double[,] data) { this.dCrData = data; }
        public void setrData(byte[,] data) { this.rData = data; }
        public void setgData(byte[,] data) { this.gData = data; }
        public void setbData(byte[,] data) { this.bData = data; }
        public void setYCrCbData(Color[,] data) { this.YCbCrData = data; }
        public void setYCrCbData2(Color[,] data) { this.YCbCrData2 = data; }
    }
}
