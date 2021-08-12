get.linear <- function(
  x.values,
  y.values,
  x.label = "X",
  y.label = "Y",
  x.short = x.label,
  y.short = y.label,
  path = NULL,
  wd = 16,
  ht = 4 / 3 * wd) {
  # If path is set - open output to that file
  if (!is.null(path))
  {
    svg(
      path,
      width = wd / 2.54,
      height = ht / 2.54,
      pointsize = fontsize,
      family = fontname
    )
  }
  
  #calculating linear fit
  
  fit <- lm(y.values ~ x.values)
  aov <- Anova(fit)
  ncv <- ncvTest(fit)
  kst <- nortest::lillie.test(fit$residuals)
  
  # Compiling report
  
  n <- length(fit$model[, 1])
  
  a <- summary(fit)$coefficients[1, 1]
  a_se <- summary(fit)$coefficients[1, 2]
  a_p <- summary(fit)$coefficients[1, 4]
  
  b <- summary(fit)$coefficients[2, 1]
  b_se <- summary(fit)$coefficients[2, 2]
  b_p <- summary(fit)$coefficients[2, 4]
  
  sigma <- summary(fit)$sigma
  rsq <- summary(fit)$adj.r.squared
  f <- aov$"F value"[1]
  f_p <- aov$"Pr(>F)"[1]
  
  chisq <- ncv$ChiSquare
  chisq_p <- ncv$p
  
  d <- kst$statistic
  d_p <- kst$p.value
  
  cat(paste("N\t\t\t\t\t", n, "\n"))
  cat(paste(
    "Intercept (logL)\t",
    format.n(a, 2),
    "±",
    format.n(a_se , 3),
    " with p = ",
    format(a_p, digits = 3),
    "\n"
  ))
  cat(paste(
    "Slope (logL)\t\t",
    format.n(b, 2),
    "±",
    format.n(b_se, 3),
    " with p = ",
    format(b_p, digits = 3),
    "\n"
  ))
  
  cat(paste("Regression error\t", format.n(sigma , 3), "\n"))
  cat(paste("Adjusted R squared\t", format.n(rsq, 2), "\n"))
  
  cat(paste(
    "F-statistic\t\t",
    format.n(f , 3),
    " with p = ",
    format(f_p, digits = 3),
    "\n"
  ))
  
  # cat("\nASSUMPTIONS CHECK\n")
  if (chisq_p <= 0.05)
    cat("[!] ")
  cat(paste(
    "Chi-square statistic \t",
    format.n(chisq  , 3),
    " with p = ",
    format(chisq_p , digits = 3),
    "\n"
  ))
  
  if (d_p <= 0.05)
    cat("[!] ")
  cat(paste(
    "D statistic\t\t",
    format.n(d , 3),
    " with p = ",
    format(d_p , 3),
    "\n"
  ))
  
  # prepare to plot
  
  xs <-
    seq(min(x.values, na.rm = TRUE),
        max(x.values, na.rm = TRUE),
        length.out = 99)
  ys <-
    predict(fit, data.frame(x.values = xs), interval = "prediction")
  ys_p <-
    predict(fit, data.frame(x.values = xs), interval = "confidence")
  
  # layout the outpus, setting appearance values
  
  layout(rbind(c(1, 1, 1), c(1, 1, 1), c(1, 1, 1), c(2, 3, 4)))
  par(mar = c(3, 3, 0, 0) + 1.1, cex = 1)
  
  plot(
    x = x.values,
    y = y.values,
    pch = 16,
    col = rgb(0, 0, 0, 1 / 4),
    ylab = y.label,
    xlab = x.label,
    bty = "l"
  )
  mtext("а",
        side = 3,
        line = -1,
        adj = 0.1, 
        cex = bigcex)
  
  t <-
    paste(
      "$",
      y.short,
      " = ",
      format.n(a, 2),
      " + ",
      format.n(b, 3),
      x.short,
      "$ | $n = ",
      n,
      "$ | $r^2 = ",
      format.n(rsq, 3),
      "$"
    )
  
  text(
    x = (max(x.values, na.rm = TRUE) - min(x.values, na.rm = TRUE)) * .5 + min(x.values, na.rm = TRUE),
    y = max(y.values, na.rm = TRUE),
    TeX(t)
  )
  
  # adding fit
  
  lines(ys[, "fit"] ~ xs,
        col = "gray20",
        lwd = 2,
        lty = "solid")
  
  lines(ys[, "lwr"] ~ xs,
        col = "gray20",
        lwd = 1,
        lty = "dotted")
  
  lines(ys[, "upr"] ~ xs,
        col = "gray20",
        lwd = 1,
        lty = "dotted")
  
  lines(ys_p[, "lwr"] ~ xs,
        col = "gray10",
        lwd = 1,
        lty = "dashed")
  
  lines(ys_p[, "upr"] ~ xs,
        col = "gray10",
        lwd = 1,
        lty = "dashed")
  
  
  plot(
    fit$fitted.values,
    fit$residuals,
    pch = 16,
    cex = 1/2,
    col = "black",
    ylab = "Остаток",
    xlab = "Расчетное значение",
    bty = "l"
  )
  mtext("б",
        side = 3,
        line = -1,
        adj = 0.1, 
        cex = bigcex)
  abline(0, 0, lwd = 1, lty = "dashed")
  
  hist(
    fit$residuals,
    xlab = "Остатки",
    freq = FALSE,
    ylab = "Частота",
    main = ""
  )
  mtext("в",
        side = 3,
        line = -1,
        adj = 0.1, 
        cex = bigcex)
  
  if (!is.null(path))
  {
    dev.off()
  }
}

get.line <- function(
  x.values,
  y.values,
  x.label = "X",
  y.label = "Y",
  x.short = x.label,
  y.short = y.label,
  path = NULL,
  wd = 16,
  ht = wd) {
  # If path is set - open output to that file
  if (!is.null(path))
  {
    svg(
      path,
      width = wd / 2.54,
      height = ht / 2.54,
      pointsize = fontsize,
      family = fontname
    )
  }
  
  #calculating linear fit
  
  fit <- lm(y.values ~ x.values - 1)
  aov <- Anova(fit)
  ncv <- ncvTest(fit)
  kst <- nortest::lillie.test(fit$residuals)
  
  # Compiling report
  
  n <- length(fit$model[, 1])
  
  a <- summary(fit)$coefficients[1, 1]
  a_se <- summary(fit)$coefficients[1, 2]
  a_p <- summary(fit)$coefficients[1, 4]
  
  sigma <- summary(fit)$sigma
  rsq <- summary(fit)$adj.r.squared
  f <- aov$"F value"[1]
  f_p <- aov$"Pr(>F)"[1]
  
  chisq <- ncv$ChiSquare
  chisq_p <- ncv$p
  
  d <- kst$statistic
  d_p <- kst$p.value
  
  cat(paste("N\t\t\t\t\t", n, "\n"))
  cat(paste(
    "Intercept (logL)\t",
    format.n(a, 3),
    "±",
    format.n(a_se , 3),
    " with p = ",
    format(a_p, digits = 3),
    "\n"
  ))
  # cat(paste("Slope (logL)\t\t",format.n( b, 2),"±",format.n( b_se, 3)," with p = ", format(b_p, digits = 3), "\n"))
  
  cat(paste("Regression error\t", format.n(sigma , 3), "\n"))
  cat(paste("Adjusted R squared\t", format.n(rsq, 3), "\n"))
  
  cat(paste(
    "F-statistic\t\t",
    format.n(f , 3),
    " with p = ",
    format(f_p, digits = 3),
    "\n"
  ))
  
  # cat("\nASSUMPTIONS CHECK\n")
  if (chisq_p <= 0.05)
    cat("[!] ")
  cat(paste(
    "Chi-square statistic \t",
    format.n(chisq  , 3),
    " with p = ",
    format(chisq_p , digits = 3),
    "\n"
  ))
  
  if (d_p <= 0.05)
    cat("[!] ")
  cat(paste(
    "D statistic\t\t",
    format.n(d , 3),
    " with p = ",
    format(d_p , 3),
    "\n"
  ))
  
  # prepare to plot
  
  xs <- seq(min(x.values, na.rm = TRUE), max(x.values, na.rm = TRUE), length.out = 99)
  ys <- predict(fit, data.frame(x.values = xs), interval = "prediction")
  ys.p <- predict(fit, data.frame(x.values = xs), interval = "confidence")
  
  # layout the outpus, setting appearance values
  
  # layout(rbind(c(1, 1, 1), c(1, 1, 1), c(1, 1, 1), c(2, 3, 4)))
  par(rbind(1,1), mar = c(3, 3, 0, 0) + 1.1, cex = 1)
  
  # lims <- c(0, max(max(x.values, na.rm = TRUE), max(y.values, na.rm = TRUE)))
  
  plot(
    x = x.values,
    y = y.values,
    # yvalues ~ xvalues,
    pch = 16,
    cex = bigcex,
    col = rgb(0, 0, 0, 1 / 4),
    ylab = y.label,
    xlab = x.label,
    # xlim = lims,
    # ylim = lims,
    bty = "L"
  )
  
  t <- c(
    paste("$\\textit{", y.short, "} = ", format.n(a, 3), "\\textit{", x.short, "}}$"),
    paste("$\\textit{n} = ", n, "$"),
    paste("$\\textit{r}^2 = ", format.n(rsq, 3), "$")
  )
  
  labxpos <- (max(x.values, na.rm = TRUE) - min(x.values, na.rm = TRUE)) * .5 + min(x.values, na.rm = TRUE)
  y.max <- max(y.values, na.rm = TRUE) 
  y.range <- y.max - min(y.values, na.rm = TRUE)
  labypos <- y.max - y.range*c(0, .05, .1)
  
  par(cex = bigcex)
  text(
    labels =  TeX(t),
    x = labxpos,
    y = labypos
  )
  par(cex = 1)
  
  
  # t <- paste("$",
  #            yshort,
  #            " = ",
  #            # format.n(a, 2),
  #            # " + ",
  #            format.n(a, 3),
  #            xshort,
  #            "$ | $N = ",
  #            n,
  #            "$ | $R^2 = ",
  #            format.n(rsq, 3),
  #            "$")
  # 
  # text(
  #   x = (max(xvalues, na.rm = TRUE) - min(xvalues, na.rm = TRUE)) * .5 + min(xvalues, na.rm = TRUE),
  #   y = max(yvalues, na.rm = TRUE),
  #   TeX(t)
  # )
  
  # adding fit
  
  lines(ys[, "fit"] ~ xs,
        col = "gray20",
        lwd = 2,
        lty = "solid")
  
  lines(ys[, "lwr"] ~ xs,
        col = "gray20",
        lwd = 1,
        lty = "dotted")
  
  lines(ys[, "upr"] ~ xs,
        col = "gray20",
        lwd = 1,
        lty = "dotted")
  
  lines(ys.p[, "lwr"] ~ xs,
        col = "gray10",
        lwd = 1,
        lty = "dashed")
  
  lines(ys.p[, "upr"] ~ xs,
        col = "gray10",
        lwd = 1,
        lty = "dashed")
  
  
  
  
  # plot(
  #   fit$fitted.values,
  #   fit$residuals,
  #   pch = 16,
  #   cex = 1/2,
  #   col = "black",
  #   ylab = "Остаток",
  #   xlab = ylabel,
  #   # xlab = "Расчетное значение",
  #   bty = "l"
  # )
  # mtext("б",
  #       side = 3,
  #       line = -1,
  #       adj = 0.1, cex = bigcex)
  # abline(0, 0, lwd = 1, lty = "dashed")
  # 
  # hist(
  #   fit$residuals,
  #   xlab = "Остатки",
  #   freq = FALSE,
  #   ylab = "Частота",
  #   main = ""
  # )
  # mtext("в",
  #       side = 3,
  #       line = -1,
  #       adj = 0.1, cex = bigcex)
  
  if (!is.null(path))
  {
    dev.off()
  }
}

get.power <- function(
  x.values,
  y.values,
  x.label = "X",
  y.label = "Y",
  base = 10,
  x.short = x.label,
  y.short = y.label,
  width = 16,
  height = 4 / 3 * width,
  intercept.decimals = 2,
  plot = FALSE,
  svg.path = NULL,
  group.name = "ALL",
  x.lim = NULL,
  y.lim = NULL,
  detailed.plot = T) {
  log.x <- log(x.values, base = base)
  log.y <- log(y.values, base = base)
  
  fit <- lm(log.y ~ log.x)
  
  n = length(fit$model[, 1])
  a <- summary(fit)$coefficients[1, 1]
  b <- summary(fit)$coefficients[2, 1]
  r2 <- summary(fit)$adj.r.squared
  
  cf <- logbtcf(fit, base = base)
  vt <- lmtest::bptest(fit)
  # vt <- ncvTest(fit)
  # vt <- ols_test_breusch_pagan(fit)
  # vt <- leveneTest(log.y ~ log.x)
  kst <- nortest::lillie.test(fit$residuals)
  
  # Compiling report
  
  output <- data.frame(
    Name = group.name,
    N = n,
    lgQ = a,
    se = summary(fit)$coefficients[1, 2],
    lgQp = summary(fit)$coefficients[1, 4],
    # inter = ifelse(a_p < .05, "", "*"),
    # CF = cf,
    Q = cf * base ^ a,
    B = b,
    se = summary(fit)$coefficients[2, 2],
    Bp = summary(fit)$coefficients[2, 4],
    # slope = ifelse(b_p < .05, "", "*"),
    RSE = summary(fit)$sigma,
    R2 = r2
    # CHIsq = vt$statistic,
    # p = vt$p.value,
    # " " = ifelse(vt$p.value > .05, "", "*"),
    # D = kst$statistic,
    # p = kst$p.value,
    # " " = ifelse(kst$p.value > .05, "", "*")
    # g = if (good) "+" else "",
    # maxl = max(x.values)
  )
  
  # Plot charts
  if (plot)
  {
    log.x.model <-
      seq(min(log.x, na.rm = TRUE),
          max(log.x, na.rm = TRUE),
          length.out = 99)
    log.y.model <-
      predict(fit, data.frame(log.x = log.x.model), interval = "prediction")
    log.y.model.ci <-
      predict(fit, data.frame(log.x = log.x.model), interval = "confidence")
    
    x.model <- base ^ log.x.model
    y.model <- cf * base ^ log.y.model
    y.model.ci <-
      cf * base ^ predict(fit, data.frame(log.x = log.x.model), interval = "confidence")
    
    # layout the output, setting appearance values
    
    # If svg.path is set - open output to that file
    
    if (!is.null(svg.path))
    {
      svg(
        svg.path,
        width = width / 2.54,
        height = height / 2.54,
        pointsize = fontsize,
        family = fontname
      )
    }
    
    
    
    if (detailed.plot)
    { layout(rbind(c(1, 1, 1), c(1, 1, 1), c(1, 1, 1), c(2, 3, 4))) }
    else { par(mfrow=c(1,1)) }
    
    par(mar = c(3, 3, 0, 0) + 1.1, cex = 1)
    
    # plot untransformed data and model
    
    plot(
      y.values ~ x.values,
      pch = 16,
      cex = 1, #bigcex,
      col = rgb(0, 0, 0, 1 / 3),
      ylab = y.label,
      xlab = x.label,
      bty = "l", xlim = x.lim, ylim = y.lim
    )
    
    # t <- c(
    #   paste("$\\textit{", y.short, "} = ", format.n(a, 3), "\\textit{", x.short, "}}$"),
    #   paste("$\\textit{n} = ", n, "$"),
    #   paste("$\\textit{r}^2 = ", format.n(rsq, 3), "$")
    # )
    
    t <- c(
      paste("$\\textit{", y.short, "} = ", format.n(cf * base ^ fit$coefficients["(Intercept)"], intercept.decimals), "\\textit{", x.short, "}^{", format.n(fit$coefficients["log.x"], 3),"}}$"),
      paste("$\\textit{n} = ", n, "$"),
      paste("$\\textit{r}^2 = ", format(r2, digits = 3, nsmall = 3), "$")
    )
    
    lab.x.pos <-
      (max(x.values, na.rm = TRUE) - min(x.values, na.rm = TRUE)) * .5 + min(x.values, na.rm = TRUE)
    y.max <- max(y.values, na.rm = TRUE)
    y.range <- y.max - min(y.values, na.rm = TRUE)
    lab.y.pos <- y.max - y.range * c(0, .05, .1)
    
    par(cex = bigcex)
    
    text(labels = TeX(t),
         x = lab.x.pos,
         y = lab.y.pos)
    
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
    
    if (detailed.plot)
    {
      
      mtext("а",
            side = 3,
            line = -1,
            adj = 0.033, cex = bigcex)
      
      plot(
        y.values ~ x.values,
        pch = 16,
        cex = 1 / 2,
        col = rgb(0, 0, 0, 1 / 3),
        ylab = y.label,
        xlab = x.label,
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
        # x = y.values,
        y = fit$residuals,
        pch = 16,
        cex = 1 / 2,
        col = "black",
        ylab = "Остаток",
        # xlab = paste("log (", y.label, ")", sep = ""),
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
    
    if (!is.null(svg.path))
    {
      dev.off()
    }
  }
  
  return (output)
}

compare.power.fits <- function(
  x.values,
  y.values,
  group,
  min.n = 5,
  
  x.label = "X",
  # Labels
  y.label = "Y",
  x.short = x.label,
  y.short = y.label,
  
  # Graphics settings
  
  width = 16,
  height = ifelse(detailed.plot, 4 / 3 * width, width),
  
  # Output
  
  path = "",
  save.as.txt = T,
  save.as.csv = T,
  save.as.svg = T,
  plot.by.group = T,
  
  # General plot options
  
  plot.general = T,
  show.prediction.bands = F,
  detailed.plot = T,
  log = "",
  legend = T,
  italicize.legend = F,
  
  # Predictions plot options
  
  lens = NULL,
  # lens = c(min(x.values, na.rm = TRUE),
  #          # Options for predictions comparison
  #          mean(x.values, na.rm = TRUE),
  #          max(x.values, na.rm = TRUE)),
  x.units = "",
  
  # Additional options
  
  base = 10,
  intercept.decimals = 3,
  pairwise = F) {
  
  dataf <- data.frame(x.values, y.values, group) %>%
    filterD(!is.na(x.values), !is.na(y.values), !is.na(group)) %>%
    mutate(log.x = log(x.values, base),
           log.y = log(y.values, base))
  
  # Create report for each level power model
  
  if (save.as.txt) {
    report <- file(paste(path, "/results.txt", sep = ""), open = "wt")
    sink(report)
  }
  
  for (gr in levels(dataf$group))
  {
    if (nrow(filter(dataf, group == gr)) < min.n)
    {
      dataf <- filterD(dataf, group != gr)
    }
  }
  
  grs <- levels(dataf$group)
  
  output <- NULL
  
  for (gr in levels(dataf$group))
  {
    gdataf <- dataf %>% filter(group == gr)
    
    out <- get.power(
      x.values = gdataf$x.values,
      y.values = gdataf$y.values,
      x.label = x.label,
      y.label = y.label,
      x.short = x.short,
      y.short = y.short,
      base = base,
      group.name = gr,
      plot = plot.by.group,
      svg.path = paste(path, "/", gr, ".svg", sep = ""),
      intercept.decimals = intercept.decimals
    )
    
    output <- rbind(output, out)
  }
  
  rownames(output) <- output[, 1]
  output <- output[,-1]
  
  options(width = 250)
  
  print.data.frame(
    output,
    quote = FALSE,
    right = TRUE,
    na.print = "",
    # print.gap = 2,
    digits = 4,
    max.print = 999
  )
  
  if (save.as.csv)
  {
    write.csv2(
      output,
      file = paste(path, "/", "results.csv", sep = ""),
      quote = FALSE,
      na = ""
    )
  }
  
  fit <- lm(log.y ~ log.x * group, data = dataf)
  cat("=== Fit: ===\n")
  print(summary(fit))
  a <- Anova(fit)
  aov <- aov(fit)
  
  common.b <- a[3, 4]
  
  ### If interaction is not significant it should be eliminated
  ### and Anova should be repeated w/o it
  if (common.b > .05)
  {
    cat("\n=== Anova (slopes): ===\n\n")
    print(a)
    
    cat("\nSlope is common. Recalculating fit with no interaction.\n\n")
    
    fit <- lm(log.y ~ log.x + group, data = dataf)
    cat("=== Fit (recalc): ===\n")
    print(summary(fit))
    
    aov <- aov(fit)
    
    a <- Anova(fit)
    cat("\n=== Anova (intercepts): ===\n\n")
    print(a)
  }
  
  if (common.b <= .05)
  {
    cat("=== Anova: ===\n")
    print(a)
    cat("* Slopes differing.")
  }
  
  common.q <- a[2, 4]
  
  if (common.q > .05)
  {
    cat("Intercept is common.")
  }
  
  if (common.q <= .05)
  {
    cat("* Intercepts differing.")
  }
  
  if (pairwise &&
      length(grs) > 2 && !(common.q > .05 && common.b > .05))
  {
    # if (common.q <= 0.05) # is intercepts deffering
    # {
    t2 <- glht(aov, linfct = mcp(group = "Tukey"))
    s <- summary(t2)
    d1 <- data.frame(diff = s$test$coefficients,
                     p = s$test$pvalues)
    d1 <- mutate(.data = d1, " " = ifelse(p <= .05, "*", ""))
    rownames(d1) <- names(s$test$coefficients)
    # cat("\n=== Intercepts pairwise comparison (glht()): ===\n")
    # print(d1, digits = 4)
    # }
    
    # t1 <- TukeyHSD(aov, which = "group")
    # cat("\n=== Pairwise comparison (Bates, \"TukeyHSD()\"): ===\n")
    # print(t1)
    
    # cat("\n=== Intercept pairwise comparison (Ogle, \"compIntercepts()\"): ===\n")
    # t3 <-compIntercepts(fit)
    # print(t3)
    
    
    # if (common.b <= 0.05) # If slopes differing
    # {
    t4 <- compSlopes(fit, order.slopes = F)
    d2 <- t4$comparisons[, c(2, 6)]
    d2 <- mutate(.data = d2, " " = ifelse(p.adj <= .05, "*", ""))
    rownames(d2) <- gsub("-", " - ", t4$comparisons[, 1])
    # cat("\n=== Slope pairwise comparison (Ogle, \"compSlopes()\"): ===\n")
    # print(d2, digits = 4)
    # }
    
    d3 <- cbind(d1, d2)
    colnames(d3) <- c("d(lgQ)", "p", " ", "d(B)", "p", " ")
    
    cat("\n=== Pairwise comparison: ===\n")
    print(d3, digits = 4)
    cat(
      "\nlgQ differences established using \"glht()\",\nB differences established using Ogles \"compSlopes()\".\n"
    )
  }
  
  # Plotting chart
  {
    if (save.as.svg)
    {
      svg(
        paste(path, "/", "results.svg", sep = ""),
        width = width / 2.54,
        height = height / 2.54,
        pointsize = fontsize,
        family = fontname
      )
    }
    
    ifelse(
      test = length(genpal) < length(grs),
      yes = pal <- colorRampPalette(genpal)(length(grs)),
      no = pal <- genpal
    )
    
    palette(pal)
    
    
    if (detailed.plot) {
      layout(rbind(c(1, 1, 1), c(1, 1, 1), c(1, 1, 1), c(2, 3, 4)))
    } else { 
      par(mfrow=c(1, 1)) 
    }
    
    par(mar = c(3, 3, 0, 0) + 1.1, cex = 1)
    
    plot(
      y.values ~ x.values,
      data = dataf,
      pch = 16,
      cex = bigcex,
      col = alpha(pal[group], .5),
      ylab = y.label,
      xlab = x.label,
      log = log,
      bty = "L"
    )
    
    tmp <-
      dataf %>% group_by(group) %>% summarise(min = min(x.values, na.rm = TRUE),
                                              max = max(x.values, na.rm = TRUE))
    
    if (common.b <= .05 || common.q <= .05)
    {
      for (i in 1:length(grs))
      {
        tmpx <- seq(tmp$min[i], tmp$max[i], length.out = 99)
        tmpy <-
          base ^ (predict(fit, data.frame(
            log.x = log(tmpx, base), group = grs[i]
          )))
        lines(tmpy ~ tmpx, col = i, lwd = 2)
      }
    }
    
    if (is.null(plot.general))
    {
      plot.general = (common.q > .05 && common.b > .05)
    }
    
    if (plot.general)
    {
      log.x <- log(x.values, base = base)
      log.y <- log(y.values, base = base)
      
      gen.fit <- lm(log.y ~ log.x, data = dataf)
      
      cat("\n=== Generalized fit: ===\n")
      print(summary(gen.fit))
      
      cf <- logbtcf(gen.fit, base = base)
      n = length(gen.fit$model[, 1])
      a <- summary(gen.fit)$coefficients[1, 1]
      b <- summary(gen.fit)$coefficients[2, 1]
      r2 <- summary(gen.fit)$adj.r.squared
      
      print(cf)
      print(cf * base ^ a)
      
      cat("\n=== Assumption of groups equality: ===\n")
      print(summary(aov(log.x ~ group, data = dataf)))
      # plot(as3)
      
      cat("\n=== Assumption of homogeneity of variance: ===\n")
      print(lmtest::bptest(gen.fit))
      
      cat("\n=== Assumption of normality of residuals: ===\n")
      # print(shapiro.test(resid(aov)))
      # print(nortest::lillie.test(resid(aov)))
      print(shapiro.test(gen.fit$residuals))
      print(nortest::lillie.test(gen.fit$residuals))
      
      log.x.model <-
        seq(min(log.x, na.rm = TRUE),
            max(log.x, na.rm = TRUE),
            length.out = 99)
      log.y.model <-
        predict(gen.fit, data.frame(log.x = log.x.model), interval = "prediction")
      log.y.model.ci <-
        predict(gen.fit, data.frame(log.x = log.x.model), interval = "confidence")
      
      x.model <- base ^ log.x.model
      y.model <- cf * base ^ log.y.model
      y.model.ci <- cf * base ^ log.y.model.ci
      
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
      
      lines(
        y.model.ci[, "lwr"] ~ x.model,
        col = "gray10",
        lwd = 1,
        lty = "dashed"
      )
      
      lines(
        y.model.ci[, "upr"] ~ x.model,
        col = "gray10",
        lwd = 1,
        lty = "dashed"
      )
      
      t <- c(
        paste(
          "$ \\textit{",
          y.short,
          "} = ",
          format.n(cf * base ^ a, intercept.decimals),
          "\\textit{", x.short, "}",
          "^ {",
          format.n(b, 3),
          "}$"
        ),
        paste("$ \\textit{n} = ", n, "$"),
        paste("$\\textit{r}^2 = ", format(
          r2, digits = 3, nsmall = 3
        ), "$")
      )
      
      lab.x.pos <-
        (max(x.values, na.rm = TRUE) - min(x.values, na.rm = TRUE)) * .5 + min(x.values, na.rm = TRUE)
      y.max <- max(y.values, na.rm = TRUE)
      y.range <- y.max - min(y.values, na.rm = TRUE)
      lab.y.pos <- y.max - y.range * c(0, .05, .1)
      
      par(cex = bigcex)
      
      text(labels = TeX(t),
           x = lab.x.pos,
           y = lab.y.pos)
      par(cex = 1)
      
      
      
      
      
      
      
      
    }
    
    if (legend)
    {
      legend(
        "topleft",
        legend = unique(dataf$group),
        ncol = ceiling(length(levels(dataf$group)) / 25),
        pch = 16,
        col = unique(dataf$group),
        inset = c(0.05, ifelse(plot.general, .2, .05)),
        bty = "n",
        text.font = ifelse(test = italicize.legend,yes = 3, no = 1), 
        y.intersp = 1
      )
    }
    else {
      addt.lw(dataf)
    }
    
    
    
    
    # xs <- seq(min(all$Length, na.rm = TRUE), max(all$Length, na.rm = TRUE), length.out = 99)
    # ys <- 0.00001036 * xs ^ 3.111
    # 
    # lines(ys ~ xs,
    #       col = "red",
    #       lwd = 2,
    #       lty = "solid")
    # 
    
    
    
    
    
    
    
    
    if (detailed.plot) {
      mtext("а",
            side = 3,
            line = -1,
            adj = 0.033, cex = bigcex)
      
      plot(
        y.values ~ x.values,
        data = dataf,
        pch = 16,
        cex = 1 / 2,
        col = alpha(pal[group], .5),
        ylab = y.label,
        xlab = x.label,
        log = "xy",
        bty = "L"
      )
      
      if (common.b <= .05 || common.q <= .05)
      {
        for (i in 1:length(grs))
        {
          tmpx <- seq(tmp$min[i], tmp$max[i], length.out = 99)
          tmpy <-
            base ^ (predict(fit, data.frame(
              log.x = log(tmpx, base), group = grs[i]
            )))
          lines(tmpy ~ tmpx, col = i, lwd = 2)
        }
      }
      
      if (plot.general)
      {
        lines(
          y.model[, "fit"] ~ x.model,
          col = "gray20",
          lwd = 2,
          lty = "solid"
        )
        lines(
          y.model[, "lwr"] ~ x.model,
          col = "gray20",
          lwd = 1,
          lty = "dotted"
        )
        lines(
          y.model[, "upr"] ~ x.model,
          col = "gray20",
          lwd = 1,
          lty = "dotted"
        )
        lines(
          y.model.ci[, "lwr"] ~ x.model,
          col = "gray10",
          lwd = 1,
          lty = "dashed"
        )
        lines(
          y.model.ci[, "upr"] ~ x.model,
          col = "gray10",
          lwd = 1,
          lty = "dashed"
        )
      }
      
      
      mtext("б",
            side = 3,
            line = -1,
            adj = 0.1, cex = bigcex)
      
      plot(
        x = fit$fitted.values,
        # x = y.values,
        y = fit$residuals,
        pch = 16,
        cex = 1 / 2,
        col = "black",
        ylab = "Остаток",
        # xlab = paste("log (", y.label, ")", sep = ""),
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
    
    
    if (save.as.txt) {
      sink()
      close(report)
    }
    
    if (save.as.svg)
      dev.off()
  }
  
  if (!is.null(lens))
    # Plotting prediction chart to file
  {
    num <- length(lens)
    rows <- ceiling(num / 2)
    
    svg(
      paste(path, "/", "plot_predictions.svg", sep = ""),
      width = 16 / 2.54,
      height = (rows * 5) / 2.54,
      pointsize = fontsize,
      family = fontname
    )
    
    par(
      # mfrow = c(3, 2),
      mfrow = c(rows, 2),
      mar = c(1, 1, 1, 1),
      oma = c(1, 4, 0, 0), 
      cex = 1
    )
    
    g <- c(1:length(grs))
    
    labs <- NULL
    
    for (i in g)
    {
      lab <- substr(grs[i], 1, 1)
      # readline(prompt = paste("Enter short for ", grs[i], " and press [enter]: ", sep = ""))
      labs <- rbind(labs, lab)
    }
    
    for (i in 1:num)
    {
      x.values <- lens[i]
      
      c <-
        base ^ predict(fit, data.frame(log.x = log(x.values, base), group = grs[g]), interval = "confidence")
      p <-
        base ^ predict(fit, data.frame(log.x = log(x.values, base), group = grs[g]), interval = "prediction")
      
      lim <- c(min(c[, 2]), max(c[, 3]))
      if (show.prediction.bands)
        lim <- c(min(p[, 2]), max(p[, 3]))
      
      plot(
        p[, 1] ~ g,
        type = ifelse(show.prediction.bands, "x.values", "p"),
        axes = FALSE,
        ylim = lim,
        # main = substitute({italic(l) == x * mm}, list(x = x.values, mm = x.units)),
        # main = paste(x.short,'=',x.values, x.units),
        main = "",
        pch = 16
      )
      
      # title(main = bquote(bolditalic(.(x.short))~" = "~bolditalic(.(lens[i]))~" "~(.(x.units))))
      title(main = bquote(bolditalic(.(x.short))~" = "~bolditalic(.(lens[i]))))
      
      if (show.prediction.bands)
      {
        segments(
          x0 = g,
          y0 = c[, 2],
          y1 = c[, 3],
          lwd = 3
        )
        arrows(
          x0 = g,
          y0 = p[, 2],
          y1 = p[, 3],
          length = 0.02,
          angle = 90,
          code = 3
        )
      }
      else
      {
        arrows(
          x0 = g,
          y0 = c[, 2],
          y1 = c[, 3],
          length = 0.02,
          angle = 90,
          code = 3
        )
      }
      
      axis(side = 2)
      
      if (i %in% c(num, num - 1)) {
        axis(
          side = 1,
          labels = labs,
          at = g)
      }
    }
    
    mtext(y.label,
          line = 2,
          side = 2,
          outer = TRUE)
    
    dev.off()
  }
  
  # return (output)
}

show.power.fits <- function(
  x.values,
  y.values,
  taxa,
  group,
  min.n = 5,
  
  x.label = "X",
  # Labels
  y.label = "Y",
  
  path) {
  base <- 10
  df <- data.frame(taxa, group, x.values, y.values) %>%
    filterD(!is.na(taxa), !is.na(group), !is.na(x.values), !is.na(y.values)) %>%
    mutate(log.x = log(x.values, base), log.y = log(y.values, base))
  
  k = 1
  
  palette(gray.colors(
    2,
    start = 0.2,
    end = 0.8,
    gamma = 2.2,
    alpha = NULL
  ))
  
  taxaun <- unique(taxa)
  
  for (m in 1:length(taxaun))
  {
    if (k == 1) {
      
      rs <- 3
      if (length(taxaun) - m < 2) rs <- (length(taxaun) - m + 1)
      
      svg(
        filename = paste(path, "/", m, ".svg", sep = ""),
        width = 16.6 / 2.54,
        height = (1.5424 + rs * 7.23) / 2.54,
        pointsize = fontsize,
        family = fontname
      )
      
      par(
        mfrow = c(rs, 2),
        mar = c(2, 2, 3, 0),
        oma = c(4, 4, 0, 0),
        cex = 1
      )
      
    }
    
    sp = taxaun[m]
    
    spd <- df %>% filterD(taxa == sp)
    wtr = unique(df$group)
    
    fit <- lm(log(y.values, 10) ~ log(x.values, 10) * group, data = spd)
    if (Anova(fit)[3, 4] > .05) {
      fit <- lm(log(y.values, 10) ~ log(x.values, 10) + group, data = spd)
    }
    
    plot(
      y.values ~ x.values,
      data = spd,
      pch = c(16, 1)[group],
      cex = 1,
      log = "",
      bty = "L"
    )
    
    tmp <-
      spd %>% group_by(group) %>% summarise(min = min(x.values, na.rm = TRUE),
                                            max = max(x.values, na.rm = TRUE))
    for (j in 1:nrow(tmp))
    {
      tmp.x <- seq(tmp$min[j], tmp$max[j], length.out = 99)
      tmp.y <- 10 ^ (predict(fit, data.frame(x.values = tmp.x, group = tmp[j, 1])))
      lines(tmp.y ~ tmp.x, lwd = 2, lty = j)
    }
    
    tit <- as.character(sp)
    title(main = bquote(bolditalic(.(tit))), adj = 0)
    
    plot(
      y.values ~ x.values,
      data = spd,
      pch = c(16, 1)[group],
      cex = 1,
      log = "xy",
      bty = "L"
    )
    
    for (j in 1:nrow(tmp))
    {
      tmp.x <- seq(tmp$min[j], tmp$max[j], length.out = 99)
      tmp.y <- 10 ^ (predict(fit, data.frame(x.values = tmp.x, group = tmp[j, 1])))
      lines(tmp.y ~ tmp.x, lwd = 2, lty = j)
    }
    
    if (k == 3 || m == length(taxaun))
    {
      mtext("Масса, мг", side = 2, outer = TRUE, line = 2.2)
      mtext("Длина, мм", side = 1, outer = TRUE, line = 2.2)
      
      dev.off()
      
      k <- 0
    }
    
    k <- k + 1
  }
}

get.exponent <- function(
  x.values,
  y.values,
  labs = NULL,
  x.label = "X",
  y.label = "Y",
  x.short = x.label,
  y.short = y.label,
  group,
  color,
  width = 16,
  intercept.decimals = 2,
  plot = TRUE,
  svg.path = NULL,
  detailed = TRUE,
  log = "",
  group.name = "ALL",
  label = "out",
  legendspot = "bottomleft",
  intercept = F,
  calc.own.only = F) {
  
  height = ifelse(detailed, 4 / 3 * width, width)
  
  base = exp(1)
  
  dataf1 <- data.frame(x.values, y.values, group, labs, color) %>%
    filterD(!is.na(x.values), !is.na(y.values), !is.na(group)) %>%
    mutate(log.y = log(y.values, base))
  
  if (calc.own.only)
  {
    dataf <- dataf1 %>% filter(group %in% c("Виштынецкое озеро", "Рыбинское водохранилище"))
  }
  else
  {
    dataf <- dataf1
  }
  
  grs <- levels(dataf$group)
  
  fit <- lm(log.y ~ x.values + 0, data = dataf)
  
  if (intercept) fit <- lm(log.y ~ x.values, data = dataf)
  
  {
    print(summary(fit))
    
    n = length(fit$model[, 1])
    b <- summary(fit)$coefficients[1, 1]
    r2 <- summary(fit)$adj.r.squared
    
    
    
    cat("\n=== Assumption of groups equality: ===\n")
    print(summary(aov(fit)))
    # plot(as3)
    
    cat("\n=== Assumption of normality of residuals: ===\n")
    # print(shapiro.test(resid(aov)))
    # print(nortest::lillie.test(resid(aov)))
    print(shapiro.test(fit$residuals))
    print(nortest::lillie.test(fit$residuals))
    
    # cat("\n=== Assumption of homogeneity of variance: ===\n")
    # hcv
    # print(lmtest::bptest(fit))
  }
  
  # Plot charts
  if (plot)
  {
    log.x.model <-
      seq(min(x.values, na.rm = TRUE),
          max(x.values, na.rm = TRUE),
          length.out = 99)
    log.y.model <-
      predict(fit, data.frame(x.values = log.x.model), interval = "prediction")
    log.y.model.ci <-
      predict(fit, data.frame(x.values = log.x.model), interval = "confidence")
    
    x.model <- log.x.model
    y.model <- base ^ log.y.model
    y.model.ci <- base ^ log.y.model.ci
    
    # layout the output, setting appearance values
    
    # If svg.path is set - open output to that file
    
    if (!is.null(svg.path))
    {
      svg(
        svg.path,
        width = width / 2.54,
        height = height / 2.54,
        pointsize = fontsize,
        family = fontname
      )
    }
    
    if (detailed) {layout(rbind(c(1, 1, 1), c(1, 1, 1), c(1, 1, 1), c(2, 3, 4)))}
    else {par(mfrow=c(1,1))}
    par(mar = c(3, 3, 0, 0) + 1.1, cex = 1)
    
    # plot untransformed data and model
    
    grs <- levels(group)
    
    ifelse(
      test = length(genpal) < length(grs),
      yes = pal <- colorRampPalette(genpal)(length(grs)),
      no = pal <- genpal
    )
    
    palette(pal)
    
    plot(
      y.values ~ x.values,
      data = dataf1,
      pch = 16,
      cex = bigcex,
      # col = rgb(0, 0, 0, 1 / 3),
      col = palette()[color],
      ylab = y.label, #expression("Коэффициент "~italic("a")),
      xlab = x.label, #expression("Коэффициент "~italic("b")),
      # ylab = bquote(.("Parameter ")~italic(a)),
      # xlab = bquote(.("Parameter ")~italic(b)),
      bty = "l",
      log = log
    )
    
    bquote(.("Коэффициент ")~italic(a))
    
    if (label == "all")
    {
      thigmophobe.labels(x = x.values,
                         y = y.values,
                         labels = labs, 
                         cex = .5,
                         offset = .3)
    }
    else if (label == "out")
    {
      prd <- base ^ predict(fit, data.frame(x.values = dataf$x.values), interval = "prediction")
      
      for (j in 1:nrow(prd))
      {
        y.up <- prd[j,"upr"]
        y.lw <- prd[j,"lwr"]
        
        if (dataf$y.values[j] > y.up || dataf$y.values[j] < y.lw)
        {
          text(x = dataf$x.values[j],
               y = dataf$y.values[j], 
               labels = stri_wrap(dataf$labs[j]),
               # cex = .4, 
               pos = 1)
        }
      }
    }
    
    legend(
      legendspot,
      legend = unique(group),
      # ncol = ceiling(length(unique(group)) / 25),
      pch = 16,
      col = unique(color),
      inset = c(0.05, .05),
      # inset = c(-0.1, 0),
      y.intersp = 1,
      bty = "n"
    )
    
    if (detailed) {
      mtext("а",
            side = 3,
            line = -1,
            adj = 0.033)
    }
    
    t <- c(
      paste("$\\textit{", y.short, "} = \\textit{e}^{", format.n(fit$coefficients["x.values"], 3), " \\textit{", x.short, "}}$"),
      paste("$\\textit{n} = ", n, "$"),
      paste("$\\textit{r}^2 = ", format(r2, digits = 3, nsmall = 3), "$")
    )
    
    x.lab.pos <- (max(dataf$x.values) - min(dataf$x.values)) * .5 + min(dataf$x.values)
    
    
    y.max <- max(dataf$y.values)
    y.min <- min(dataf$y.values)
    y.range <- y.max - y.min
    
    y.lab.pos <- y.max - y.range * c(0, .05, .1)
    
    if (log == "y" || log == "xy")
    {
      y.max.lg <- log(y.max, base)
      y.min.lg <- log(y.min, base)
      y.range.lg <- y.max.lg - y.min.lg
      y.lab.pos <- base ^ (y.max.lg - y.range.lg * c(0, .05, .1))
    }
    
    par(cex = bigcex)
    text(labels = TeX(t),
         x = x.lab.pos,
         y = y.lab.pos)
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
      plot(
        y.values ~ x.values,
        pch = 16,
        cex = 1 / 2,
        # col = rgb(0, 0, 0, 1 / 3),
        col = palette()[group],
        ylab = y.label,
        xlab = x.label,
        bty = "l",
        log = "y"
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
        # x = y.values,
        y = fit$residuals,
        pch = 16,
        cex = 1 / 2,
        col = "black",
        ylab = "Остаток",
        # xlab = paste("log (", y.label, ")", sep = ""),
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
    
    if (!is.null(svg.path))
    {
      dev.off()
    }
  }
  
}
