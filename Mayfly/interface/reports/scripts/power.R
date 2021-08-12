function(
  xvalues,
  yvalues,
  xlabel = "X",
  ylabel = "Y",
  width = 16,
  height = 4 / 3 * width,
  path = NULL,
  detailed = T) {
  
  log.x <- log(xvalues, base = 10)
  log.y <- log(yvalues, base = 10)
  
  fit <- lm(log.y ~ log.x)
  
  n = length(fit$model[, 1])
  a <- summary(fit)$coefficients[1, 1]
  b <- summary(fit)$coefficients[2, 1]
  
  
  log.x.model <- seq(min(log.x, na.rm = TRUE), max(log.x, na.rm = TRUE), length.out = 99)
  log.y.model <- predict(fit, data.frame(log.x = log.x.model), interval = "prediction")
  log.y.model.ci <- predict(fit, data.frame(log.x = log.x.model), interval = "confidence")
  
  x.model <- 10 ^ log.x.model
  y.model <- 10 ^ log.y.model
  y.model.ci <- 10 ^ predict(fit, data.frame(log.x = log.x.model), interval = "confidence")
  
  if (!is.null(path))
  {
    svg(
      path,
      width = width / 2.54,
      height = height / 2.54
      # pointsize = fontsize,
      # family = fontname
    )
  }
  
  if (detailed)
  {
    layout(rbind(c(1, 1, 1), c(1, 1, 1), c(1, 1, 1), c(2, 3, 4))) 
  }
  else
  {
    par(mfrow=c(1,1))
  }
  
  par(mar = c(3, 3, 0, 0) + 1.1, cex = 1)
  
  plot(
    yvalues ~ xvalues,
    pch = 16,
    cex = 1,
    col = rgb(0, 0, 0, 1 / 3),
    ylab = ylabel,
    xlab = xlabel,
    bty = "l"
  )
  
  par(cex = 1)
  
  lines(y.model[, "fit"] ~ x.model,
        col = "gray20",
        lwd = 2,
        lty = "solid")
  
  lines(y.model[, "lwr"] ~ x.model,
        col = "gray20",
        lwd = 1,
        lty = "dotted")
  
  lines(y.model[, "upr"] ~ x.model,
        col = "gray20",
        lwd = 1,
        lty = "dotted")
  
  lines(y.model.ci[, "lwr"] ~ x.model,
        col = "gray10",
        lwd = 1,
        lty = "dashed")
  
  lines(y.model.ci[, "upr"] ~ x.model,
        col = "gray10",
        lwd = 1,
        lty = "dashed")
  
  if (detailed)
  {
    mtext("а",
          side = 3,
          line = -1,
          adj = 0.033, cex = bigcex)
    
    plot(
      yvalues ~ xvalues,
      pch = 16,
      cex = 1 / 2,
      col = rgb(0, 0, 0, 1 / 3),
      ylab = ylabel,
      xlab = xlabel,
      bty = "l",
      log = "xy"
    )
    
    lines(y.model[, "fit"] ~ x.model,
          col = "gray20",
          lwd = 2,
          lty = "solid")
    lines(y.model[, "lwr"] ~ x.model,
          col = "gray20",
          lwd = 1,
          lty = "dotted")
    lines(y.model[, "upr"] ~ x.model,
          col = "gray20",
          lwd = 1,
          lty = "dotted")
    lines(y.model.ci[, "lwr"] ~ x.model,
          col = "gray10",
          lwd = 1,
          lty = "dashed")
    lines(y.model.ci[, "upr"] ~ x.model,
          col = "gray10",
          lwd = 1,
          lty = "dashed")
    mtext("б",
          side = 3,
          line = -1,
          adj = 0.1, cex = bigcex)
    
    plot(
      x = fit$fitted.values,
      # x = yvalues,
      y = fit$residuals,
      pch = 16,
      cex = 1 / 2,
      col = "black",
      ylab = "Остаток",
      # xlab = paste("log (", ylabel, ")", sep = ""),
      xlab = "Расчетное значение",
      # log = "x",
      bty = "l"
    )
    
    mtext("в",
          side = 3,
          line = -1,
          adj = 0.1, cex = bigcex)
    
    abline(0, 0, lwd = 1, lty = "dashed")
    
    hist(
      fit$residuals,
      xlab = "Остатки",
      freq = FALSE,
      ylab = "Частота",
      main = ""
    )
    
    mtext("г",
          side = 3,
          line = -1,
          adj = 0.1, cex = bigcex)
  }
  
  if (!is.null(path))
  {
    dev.off()
  }
}