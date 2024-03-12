﻿// This file was auto-generated by ML.NET Model Builder.

using Microsoft.ML.Data;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommentPrediction
{
    public partial class MLModel1
    {
        /// <summary>
        /// Permutation feature importance (PFI) is a technique to determine the importance 
        /// of features in a trained machine learning model. PFI works by taking a labeled dataset, 
        /// choosing a feature, and permuting the values for that feature across all the examples, 
        /// so that each example now has a random value for the feature and the original values for all other features.
        /// The evaluation metric (e.g. R-squared) is then calculated for this modified dataset, 
        /// and the change in the evaluation metric from the original dataset is computed. 
        /// The larger the change in the evaluation metric, the more important the feature is to the model.
        /// 
        /// PFI typically takes a long time to compute, as the evaluation metric is calculated 
        /// many times to determine the importance of each feature. 
        /// 
        /// </summary>
        /// <param name="mlContext">The common context for all ML.NET operations.</param>
        /// <param name="trainData">IDataView used to evaluate the model.</param>
        /// <param name="model">Model to evaluate.</param>
        /// <param name="labelColumnName">Label column being predicted.</param>
        /// <returns>A list of each feature and its importance.</returns>
        public static List<Tuple<string, double>> CalculatePFI(MLContext mlContext, IDataView trainData, ITransformer model, string labelColumnName)
        {
            var preprocessedTrainData = model.Transform(trainData);

            var permutationFeatureImportance =
         mlContext.MulticlassClassification
         .PermutationFeatureImportance(
                 model,
                 preprocessedTrainData,
                 labelColumnName: labelColumnName);

            var featureImportanceMetrics =
                 permutationFeatureImportance
                 .Select((kvp) => new { kvp.Key, kvp.Value.MacroAccuracy })
                 .OrderByDescending(myFeatures => Math.Abs(myFeatures.MacroAccuracy.Mean));

            var featurePFI = new List<Tuple<string, double>>();
            foreach (var feature in featureImportanceMetrics)
            {
                var pfiValue = Math.Abs(feature.MacroAccuracy.Mean);
                featurePFI.Add(new Tuple<string, double>(feature.Key, pfiValue));
            }

            return featurePFI;
        }
    }
}


